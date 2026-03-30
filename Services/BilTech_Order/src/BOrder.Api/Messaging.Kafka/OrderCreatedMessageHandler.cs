using System.Data;
using BOrder.Domain.Interfaces;
using BOrder.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Messaging.Kafka;

public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> logger, IOrderRepository repository)
    : IMessageHandler<OrderEntity>
{
    public async Task HandleAsync(OrderEntity message, CancellationToken cancellationToken)
    {
        var order = new OrderEntity()
        {
            Id = message.Id,
            ShopId = message.ShopId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = "Created",
            Items = message.Items.Select(item => new OrderItemEntity
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                OrderId = message.Id
            }).ToList()
        };

        await repository.CreateOrder(order, cancellationToken);
        logger.LogInformation("OrderCreated message received {OrderCreated}", order.Id);
    }
}