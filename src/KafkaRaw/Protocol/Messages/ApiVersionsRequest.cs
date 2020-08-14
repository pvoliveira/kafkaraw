/// https://kafka.apache.org/protocol#The_Messages_ApiVersions
using System;

namespace KafkaRaw.Protocol.Messages
{
    /// ApiVersions Request (Version: 3)
    public readonly struct ApiVersionsRequest : BaseRequestMessage
    {
        public ApiVersionsRequest(
            short requestApiKey,
            short requestApiVersion,
            int correlationId,
            string clientId)
        {
            RequestApiKey = requestApiKey;
            RequestApiVersion = requestApiVersion;
            CorrelationId = correlationId;
            ClientId = clientId;
        }
        
        public string ClientId { get; }

        public short RequestApiKey { get; }

        public short RequestApiVersion { get; }

        public int CorrelationId { get; }
    }
}

// Request Header v2 => request_api_key request_api_version correlation_id client_id TAG_BUFFER
//   request_api_key => INT16
//   request_api_version => INT16
//   correlation_id => INT32
//   client_id => NULLABLE_STRING
