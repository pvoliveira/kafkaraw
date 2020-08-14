using KafkaRaw.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KafkaRaw
{
    public static class KafkaRawExtensions
    {
        public static IServiceCollection AddKafkaRawClient(this IServiceCollection services, string brokers) =>
            services.AddSingleton<IKafkaRawClient>(p => 
                    new KafkaRawClient(p, brokers, p.GetService<ILogger<KafkaRawClient>>()));
    }
}
