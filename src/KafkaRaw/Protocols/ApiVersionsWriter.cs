using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;
using Bedrock.Framework.Protocols;
using KafkaRaw.Protocols.Messages;

namespace KafkaRaw.Protocols
{
    public class ApiVersionsWriter : IMessageWriter<ApiVersionsRequest>
    {
        public void WriteMessage(ApiVersionsRequest message, IBufferWriter<byte> output)
        {
            var csnBytes = Encoding.UTF8.GetBytes(message.ClientSoftwareName);
            var csnN = VarintBitConverter.GetVarintBytes((uint)(csnBytes.Length + 1));

            var csvBytes = Encoding.UTF8.GetBytes(message.ClientSoftwareVersion);
            var csvN = VarintBitConverter.GetVarintBytes((uint)(csvBytes.Length + 1));

            var size = csnN.Length + csnBytes.Length +  csvN.Length + csvBytes.Length;

            var sizeBuffer = output.GetSpan(4);
            BinaryPrimitives.WriteInt32BigEndian(sizeBuffer, size);
            output.Advance(4);

            var csnBytesBuffer = output.GetSpan(csnBytes.Length);
            csnBytes.AsSpan().CopyTo(csnBytesBuffer);
            output.Advance(csnBytes.Length);

            var csnNBuffer = output.GetSpan(csnN.Length);
            csnN.AsSpan().CopyTo(csnNBuffer);
            output.Advance(csnN.Length);

            var csvBytesBuffer = output.GetSpan(csvBytes.Length);
            csvBytes.AsSpan().CopyTo(csvBytesBuffer);
            output.Advance(csvBytes.Length);

            var csvNBuffer = output.GetSpan(csvN.Length);
            csvN.AsSpan().CopyTo(csvNBuffer);
            output.Advance(csvN.Length);
        }
    }
}