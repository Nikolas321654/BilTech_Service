using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IWarehouseTransferService
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId, CancellationToken cancellationToken);

    public Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId,
        CancellationToken cancellationToken);

    public Task CreateWarehouseOrder(Guid shopId, Guid warehouseId, CancellationToken cancellationToken);

    public Task UpdateWarehouseOrder(Guid shopId, Guid orderId, OrderStatus status, DateTime? deliveryDate,
        CancellationToken cancellationToken);

    public Task DeleteWarehouseOrder(Guid shopId, Guid orderId, CancellationToken cancellationToken);

    public Task AddProductToWarehouseOrder(Guid shopId, Guid orderId, Guid productId, int quantity,
        CancellationToken cancellationToken);
}