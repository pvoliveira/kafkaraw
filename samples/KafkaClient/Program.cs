using System.Threading.Tasks;
using System.Text.Json;
using KafkaRaw;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KafkaClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var provider = new ServiceCollection()
                .AddLogging(builder =>
                    builder
                        .SetMinimumLevel(LogLevel.Debug))
                .AddKafkaRawClient("localhost:9092")
                .BuildServiceProvider();

            await provider.GetService<IKafkaRawClient>().ConnectAsync();

            var r = await provider.GetService<IKafkaRawClient>().GetApiVersions();

            string apis = JsonSerializer.Serialize(r, new JsonSerializerOptions { WriteIndented = true });

            provider.GetService<ILogger<Program>>().LogDebug($"ApiVersions: {apis}");

            Console.ReadKey();
        }
    }
}
