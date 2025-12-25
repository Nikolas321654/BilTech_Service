using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IWarehouseTransferRepository
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId);
    public Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId);
    public Task CreateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest);
    public Task UpdateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest);
    public Task DeleteWarehouseOrder(Guid shopId, Guid orderId);
}