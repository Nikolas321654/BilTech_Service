using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IWarehouseTransferRepository
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId, CancellationToken cancellationToken);

    public Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId,
        CancellationToken cancellationToken);

    public Task CreateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest,
        CancellationToken cancellationToken);

    public void UpdateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest,
        CancellationToken cancellationToken);

    public Task DeleteWarehouseOrder(Guid shopId, Guid orderId, CancellationToken cancellationToken);
    public Task AddProductToOrder(WarehouseOrder order, CancellationToken cancellationToken);

    public Task<WarehouseOrder?> GetWarehouseOrderProduct(Guid orderId, Guid productId,
        CancellationToken cancellationToken);

    public Task UpdateWarehouseOrderProduct(Guid productId, Guid orderId, int quantity,
        CancellationToken cancellationToken);
}