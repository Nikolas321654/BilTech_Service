using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IWarehouseTransferRepository
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId);
    public Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId);
    public Task CreateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest);
    public void UpdateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest);
    public Task DeleteWarehouseOrder(Guid shopId, Guid orderId);
    public Task AddProductToOrder(WarehouseOrder order);
    public Task<WarehouseOrder?> GetWarehouseOrderProduct(Guid orderId, Guid productId);
    public Task UpdateWarehouseOrderProduct(Guid productId, Guid orderId, int quantity);
}