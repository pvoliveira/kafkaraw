/// https://kafka.apache.org/protocol#The_Messages_ApiVersions
using System;

namespace KafkaRaw.Protocols.Messages
{
    /// ApiVersions Request (Version: 3)
    public readonly struct ApiVersionsRequest
    {
        public ApiVersionsRequest(short requestApiKey, short requestApiVersion, string clientId) : this()
        {
            RequestApiKey = requestApiKey;
            RequestApiVersion = requestApiVersion;
            ClientId = clientId;
        }

        public short RequestApiKey { get; }
        public short RequestApiVersion { get; }
        public int CorrelationId { get; }
        public string ClientId { get; }
    }
}

// Request Header v2 => request_api_key request_api_version correlation_id client_id TAG_BUFFER
//   request_api_key => INT16
//   request_api_version => INT16
//   correlation_id => INT32
//   client_id => NULLABLE_STRING
