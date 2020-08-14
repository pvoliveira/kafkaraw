using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KafkaRaw.Protocol.Messages;
using Microsoft.Extensions.Logging;

namespace KafkaRaw
{
    public class KafkaRawClient : IKafkaRawClient
    {
        private readonly ILogger<KafkaRawClient> _logger;
        private readonly IServiceProvider _serviceProvider;
        private DnsEndPoint[] _initialBrokers;
        private ConcurrentDictionary<DnsEndPoint, Connection> _connections =
            new ConcurrentDictionary<DnsEndPoint, Connection>();

        public KafkaRawClient(IServiceProvider serviceProvider, string initialBrokers, ILogger<KafkaRawClient> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            LoadInitialBrokers(initialBrokers);
        }

        private void LoadInitialBrokers(string initialBrokers)
        {
            if (string.IsNullOrEmpty(initialBrokers))
            {
                throw new ArgumentException("An broker should be given.", nameof(initialBrokers));
            }

            _initialBrokers = initialBrokers
                .Split(';')
                .Select(b =>
                {
                    var address = b.Split(':');
                    return new DnsEndPoint(address[0], int.Parse(address[1]));
                })
                .ToArray();
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var conn in _connections)
            {
                await conn.Value.DisposeAsync();
            }
        }

        public async Task ConnectAsync()
        {
            foreach (var host in _initialBrokers)
            {
                _connections.TryAdd(host, await makeConnection(host));
            }

            _logger.LogInformation("Starting Kafka client.");

            async Task<Connection> makeConnection(DnsEndPoint h)
            {
                var client = new ClientBuilder(_serviceProvider)
                                .UseSockets()
                                .UseConnectionLogging()
                                .Build();

                var connection = await client.ConnectAsync(h);
                _logger.LogInformation($"Connected to {connection.LocalEndPoint}");

                return connection;
            };
        }

        public async Task<ApiVersionsResponse> GetApiVersions()
        {
            var request = new ApiVersionsRequest(18, 0 , 1, "kafkaraw/0.1");

            var conn = _connections.First();

            var protocol = Cache<Protocol.ApiVersions>.Instance;
            var reader = conn.Value.CreateReader();
            var writer = conn.Value.CreateWriter();

            await writer.WriteAsync(protocol, request);

            var result = await reader.ReadAsync(protocol);

            reader.Advance();

            return result.Message;
        }
    
        private static class Cache<T> where T : new()
        {
            public static readonly T Instance = new T();
        }
    }
}
