using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Kafka;

public static class Extensions
{
    public static IServiceCollection AddConsumer<TMessage, THandler>(
        this IServiceCollection servicesCollection, IConfigurationSection configurationSection)
        where THandler : class, IMessageHandler<TMessage>
    {
        servicesCollection.Configure<KafkaSettings>(configurationSection);
        servicesCollection.AddScoped<IMessageHandler<TMessage>, THandler>();
        servicesCollection.AddHostedService<KafkaConsumer<TMessage>>();
        return servicesCollection;
    }
}