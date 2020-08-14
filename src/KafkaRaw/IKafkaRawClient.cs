using System;
using System.Threading.Tasks;
using KafkaRaw.Protocol.Messages;

namespace KafkaRaw
{
    public interface IKafkaRawClient : IAsyncDisposable
    {
        Task ConnectAsync();

        Task<ApiVersionsResponse> GetApiVersions();
    }
}
