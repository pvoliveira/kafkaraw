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
            
            output.Write(csnN);
            output.Write(csnBytes);
            output.Write(csvN);
            output.Write(csvBytes);
        }
    }
}