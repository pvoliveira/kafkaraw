using System;
using System.Threading.Tasks;
using KafkaRaw.Protocols.Messages;

namespace KafkaRaw.Interfaces
{
    public interface IKafkaRawClient : IAsyncDisposable
    {
        Task ConnectAsync();

        Task<ApiVersionsResponse> GetApiVersions();
    }
}
