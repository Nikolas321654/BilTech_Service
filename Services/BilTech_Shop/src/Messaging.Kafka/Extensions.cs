using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Kafka;

public static class Extensions
{
    public static void AddProducer<TMessage>(this IServiceCollection services,
        IConfigurationSection configurationSection)
    {
        services.Configure<KafkaSettings>(configurationSection);
        services.AddSingleton<IKafkaProducer<TMessage>, KafkaProducer<TMessage>>();
    }
}