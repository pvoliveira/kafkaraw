using System;
using System.Threading.Tasks;
using KafkaRaw.Interfaces;
using Microsoft.Extensions.Logging;

namespace KafkaRaw
{
    public class KafkaRawClient : IKafkaRawClient
    {
        private readonly ILogger<KafkaRawClient> _logger;
        private TaskCompletionSource<object> tcsClient = new TaskCompletionSource<object>();

        public KafkaRawClient(ILogger<KafkaRawClient> logger)
        {
            _logger = logger;
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync()
        {
            _logger.LogInformation("Starting Kafka client.");
            return tcsClient.Task;
        }
    }
}
