using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;
using Bedrock.Framework.Protocols;
using KafkaRaw.Protocols.Messages;

namespace KafkaRaw.Protocols
{
    public class ApiVersionsReader : IMessageReader<ApiVersionsResponse>
    {
        public bool TryParseMessage(in ReadOnlySequence<byte> input, ref SequencePosition consumed, ref SequencePosition examined, out ApiVersionsResponse message)
        {
            var reader = new SequenceReader<byte>(input);
            if (!reader.TryReadBigEndian(out int length) || input.Length < length)
            {
                message = default;
                return false;
            }

            var hasErrorCode = reader.TryReadBigEndian(out short errorCode);
            if (hasErrorCode) reader.Advance(2);

            var hasApiKeys = reader.TryReadBigEndian(out int apiKeysLength);
            ApiKeys[] apiKeys;
            if (hasApiKeys)
            {
                reader.Advance(4);

                apiKeys = new ApiKeys[apiKeysLength];
                for (int i = 0; i < apiKeysLength; i++)
                {
                    reader.TryReadBigEndian(out short apiKey);
                    reader.TryReadBigEndian(out short minVersion);
                    reader.TryReadBigEndian(out short maxVersion);
                    apiKeys[i] = new ApiKeys(apiKey, minVersion, maxVersion);
                    reader.Advance(16);
                }
            }

            var hasThrottleTime = reader.TryReadBigEndian(out int throttleTimeMs);
            reader.Advance(4);

            message = new ApiVersionsResponse(errorCode, apiKeys, throttleTimeMs);

            consumed = reader.Position;
            examined = consumed;
            return true;
        }
    }
}