using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrder(Guid warehouseId, List<(Guid ProductId, int Quantity)> items, CancellationToken cancellationToken);
    Task MarkAsLeftWarehouse(Guid orderId, CancellationToken cancellationToken);
    Task<Order?> GetOrderById(Guid orderId, CancellationToken cancellationToken);
    IQueryable<Order> GetWarehouseOrders(Guid warehouseId, CancellationToken cancellationToken);
}
