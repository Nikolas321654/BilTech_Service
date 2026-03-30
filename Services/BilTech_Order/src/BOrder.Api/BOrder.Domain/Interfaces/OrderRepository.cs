using BOrder.Domain.Models;

namespace BOrder.Domain.Interfaces;

public interface IOrderRepository
{
    public Task UpdateOrder(OrderEntity order);
    public Task CreateOrder(OrderEntity order, CancellationToken cancellationToken);
    public Task<OrderEntity?> GetOrder(Guid id, CancellationToken cancellationToken);
}