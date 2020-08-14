using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;
using Bedrock.Framework.Protocols;
using KafkaRaw.Protocols.Messages;

namespace KafkaRaw.Protocols
{
    public class ApiVersions :
        IMessageReader<ApiVersionsResponse>,
        IMessageWriter<ApiVersionsRequest>
    {
        public bool TryParseMessage(in ReadOnlySequence<byte> input, ref SequencePosition consumed, ref SequencePosition examined, out ApiVersionsResponse message)
        {
            var reader = new SequenceReader<byte>(input);
            if (!reader.TryReadBigEndian(out int length)
                || input.Length < length)
            {
                message = default;
                return false;
            }

            reader.TryReadBigEndian(out int correlationId);

            if (!reader.TryReadBigEndian(out short errorCode)
                || errorCode != 0)
            {
                throw new Exception($"Error Core: {errorCode}");
            }

            ApiKeys[] apiKeys = null;
            if (reader.TryReadBigEndian(out int apiKeysLength))
            {

                apiKeys = new ApiKeys[apiKeysLength];
                for (int i = 0; i < apiKeysLength; i++)
                {
                    reader.TryReadBigEndian(out short apiKey);
                    reader.TryReadBigEndian(out short minVersion);
                    reader.TryReadBigEndian(out short maxVersion);
                    apiKeys[i] = new ApiKeys(apiKey, minVersion, maxVersion);
                }
            }

            message = new ApiVersionsResponse(errorCode, apiKeys);

            consumed = reader.Position;
            examined = consumed;
            return true;
        }

        public void WriteMessage(ApiVersionsRequest message, IBufferWriter<byte> output)
        {
            var cltIdBytes = Encoding.UTF8.GetBytes(message.ClientId);

            var size = 14 + cltIdBytes.Length;

            // Size
            var sizeBuffer = output.GetSpan(4);
            BinaryPrimitives.WriteInt32BigEndian(sizeBuffer, size);
            output.Advance(4);

            // Header - RequestApiKey
            var rakBuffer = output.GetSpan(2);
            BinaryPrimitives.WriteInt16BigEndian(rakBuffer, message.RequestApiKey);
            output.Advance(2);

            // Header - RequestApiVersion
            var ravBuffer = output.GetSpan(2);
            BinaryPrimitives.WriteInt16BigEndian(ravBuffer, message.RequestApiVersion);
            output.Advance(2);

            // Header - CorrelationId
            var crIdBuffer = output.GetSpan(4);
            BinaryPrimitives.WriteInt32BigEndian(crIdBuffer, message.CorrelationId);
            output.Advance(4);

            // Headers - ClientId
            var cltIdNBuffer = output.GetSpan(2);
            BinaryPrimitives.WriteInt16BigEndian(cltIdNBuffer, (short)cltIdBytes.Length);
            output.Advance(2);
            output.Write(cltIdBytes);
        }
    }
}