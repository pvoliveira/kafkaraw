using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KafkaRaw
{

    /*
    teoria de como funcionará os pipelines

                  |                 Connection                  |
                  |          PipeIn          |      Socket      |        
    |    Reader   | > reader (pipe) writer < | socket (receive) |
    |             |          PipeOut         |                  | 
    |    Writer   | > writer (pipe) reader > | socket (writer)  |
    */

    internal class Connection : IAsyncDisposable
    {
        private readonly ILogger<Connection> logger;
        private Socket socket;
        private DnsEndPoint endpoint;
        private Pipe pipeOut;
        private Pipe pipeIn;

        public Connection(DnsEndPoint endpoint, ILogger<Connection> logger)
        {
            this.endpoint = endpoint;
            this.logger = logger;
        }

        public async Task ConnectAsync()
        {
            this.socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            await this.socket.ConnectAsync(this.endpoint);

            this.pipeOut = new Pipe();
            this.pipeIn = new Pipe();
        }

        private async Task ProcessLinesAsync(Socket socket, CancellationToken stopConsuming)
        {
            Task writing = WriteToPipeOutAsync(socket, pipeOut.Writer, stopConsuming);
            Task reading = ReadFromPipeOutAsync(pipeOut.Reader);

            await Task.WhenAll(reading, writing);
        }

        private async Task WriteToPipeOutAsync(Socket socket, PipeWriter writer, CancellationToken stopToken)
        {
            const int minimumBufferSize = 512;

            while (!stopToken.IsCancellationRequested)
            {
                // Allocate at least 512 bytes from the PipeWriter.
                Memory<byte> memory = writer.GetMemory(minimumBufferSize);
                try
                {
                    int bytesRead = await socket.ReceiveAsync(memory, SocketFlags.None);
                    if (bytesRead == 0)
                    {
                        await Task.Delay(1);
                        continue;
                    }
                    // Tell the PipeWriter how much was read from the Socket.
                    writer.Advance(bytesRead);
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "Error when reading from connection");
                    break;
                }

                // Make the data available to the PipeReader.
                FlushResult result = await writer.FlushAsync();

                if (result.IsCompleted)
                {
                    break;
                }
            }

            // By completing PipeWriter, tell the PipeReader that there's no more data coming.
            await writer.CompleteAsync();
        }

        private async Task ReadFromPipeOutAsync(PipeReader reader)
        {
            while (true)
            {
                ReadResult result = await reader.ReadAsync();
                ReadOnlySequence<byte> buffer = result.Buffer;

                while (TryParseResponse(ref buffer, out  var line))
                {
                    // Process the line.
                    // ProcessLine(line);
                }

                // Tell the PipeReader how much of the buffer has been consumed.
                reader.AdvanceTo(buffer.Start, buffer.End);

                // Stop reading if there's no more data coming.
                if (result.IsCompleted)
                {
                    break;
                }
            }

            // Mark the PipeReader as complete.
            await reader.CompleteAsync();
        }

        private bool TryParseResponse(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
        {
            var reader = new SequenceReader<byte>(buffer);
            line = ReadOnlySequence<byte>.Empty;

            if (!reader.TryReadBigEndian(out int length)
                || buffer.Length < length)
            {
                return false;
            }

            if (!reader.TryReadBigEndian(out int correlationId))
            {
                return false;
            }

            if (!reader.TryReadBigEndian(out short errorCode)
                || errorCode != 0)
            {
                throw new Exception($"Error Core: {errorCode}");
            }

            return true;
        }

        
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
