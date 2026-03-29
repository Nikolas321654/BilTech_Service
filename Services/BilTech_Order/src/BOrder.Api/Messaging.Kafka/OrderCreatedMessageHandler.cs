using Microsoft.Extensions.Logging;

namespace Messaging.Kafka;

public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> logger) : IMessageHandler<OrderCreated>
{
    public async Task HandleAsync(OrderCreated message, CancellationToken cancellationToken)
    {
        logger.LogInformation("OrderCreated message received {OrderCreated}", message);
        await Task.CompletedTask;
    }
}