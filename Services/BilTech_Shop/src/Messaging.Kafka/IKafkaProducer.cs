namespace Messaging.Kafka;

public interface IKafkaProducer<TMessage> : IDisposable
{
    Task ProduceAsync(string key, TMessage message, CancellationToken cancellationToken);
}