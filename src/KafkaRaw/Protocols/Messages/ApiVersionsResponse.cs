/// https://kafka.apache.org/protocol#The_Messages_ApiVersions
using System;

namespace KafkaRaw.Protocols.Messages
{
    /// <summary>ApiVersions Response (Version: 3)</summary>
    public struct ApiVersionsResponse
    {
        public ApiVersionsResponse(short errorCode, ApiKeys[] apiKeys, int throttleTimeMs)
        {
            ErrorCode = errorCode;
            ApiKeys = apiKeys;
            ThrottleTimeMs = throttleTimeMs;
        }

        public short ErrorCode { get; }

        public ApiKeys[] ApiKeys { get; }

        public int ThrottleTimeMs { get; }
    }

    public struct ApiKeys
    {
        public ApiKeys(short apiKey, short minVersion, short maxVersion)
        {
            ApiKey = apiKey;
            MinVersion = minVersion;
            MaxVersion = maxVersion;
        }

        public short ApiKey { get; }

        public short MinVersion { get; }

        public short MaxVersion { get; }
    }
}

// error_code => INT16
//   api_keys => api_key min_version max_version TAG_BUFFER 
//     api_key => INT16
//     min_version => INT16
//     max_version => INT16
//   throttle_time_ms => INT32