using KafkaRaw.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaRaw
{
    public static class KafkaRawExtensions
    {
        public static IServiceCollection AddKafkaRawClient(this IServiceCollection services, string brokers) =>
            services.AddSingleton<IKafkaRawClient>();
    }
}
