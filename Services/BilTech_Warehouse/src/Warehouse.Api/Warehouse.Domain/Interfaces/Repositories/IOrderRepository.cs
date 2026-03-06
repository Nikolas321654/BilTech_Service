using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetOrderById(Guid orderId, CancellationToken cancellationToken);
    Task CreateOrder(Order order, CancellationToken cancellationToken);
    Task UpdateOrder(Order order, CancellationToken cancellationToken);
    IQueryable<Order> GetWarehouseOrders(Guid warehouseId, CancellationToken cancellationToken);
}
