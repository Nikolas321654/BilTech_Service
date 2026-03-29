using System.Text.Json;
using Confluent.Kafka;

namespace Messaging.Kafka;

public class KafkaValueDeserializer<TMessage> : IDeserializer<TMessage>
{
    public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.IsEmpty) return default!;
        
        return JsonSerializer.Deserialize<TMessage>(data) ?? throw new JsonException("Deserialized message is null");
    }
}