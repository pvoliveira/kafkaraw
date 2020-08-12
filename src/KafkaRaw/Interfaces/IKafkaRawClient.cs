using System;
using System.Threading.Tasks;

namespace KafkaRaw.Interfaces
{
    interface IKafkaRawClient : IAsyncDisposable
    {
        Task StartAsync();
    }
}
