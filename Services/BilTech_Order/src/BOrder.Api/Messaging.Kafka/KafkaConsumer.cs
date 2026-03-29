using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka;

public class KafkaConsumer<TMessage> : BackgroundService
{
    private readonly IMessageHandler<TMessage> _messageHandler;
    private readonly string _topic;
    private readonly IConsumer<string, TMessage> _consumer;

    public KafkaConsumer(IOptions<KafkaSettings> kafkaSettings, IMessageHandler<TMessage> messageHandler)
    {
        _messageHandler = messageHandler;
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers,
            GroupId = kafkaSettings.Value.Topic,
        };
        _topic = kafkaSettings.Value.Topic;
        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaValueDeserializer<TMessage>())
            .Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumerAsunc(stoppingToken), stoppingToken);
    }

    private Task? ConsumerAsunc(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                _messageHandler.HandleAsync(consumeResult.Message.Value, stoppingToken);
                Console.WriteLine(consumeResult.Message.Value);
            }

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }
}