/// https://kafka.apache.org/protocol#The_Messages_ApiVersions
using System;

namespace KafkaRaw.Protocols.Messages
{
    /// <summary>ApiVersions Request (Version: 3)</summary>
    public struct ApiVersionsRequest
    {
        public ApiVersionsRequest(string clientSoftwareName, string clientSoftwareVersion)
        {
            ClientSoftwareName = clientSoftwareName;
            ClientSoftwareVersion = clientSoftwareVersion;
        }

        public string ClientSoftwareName { get; }

        public string ClientSoftwareVersion { get; }
    }
}