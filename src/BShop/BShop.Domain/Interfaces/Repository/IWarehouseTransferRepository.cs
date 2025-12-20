using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IWarehouseTransferRepository
{
    public Task<IQueryable<WarehouseTransferRequest>> GetAllWarehouseOrders();
    public Task<WarehouseTransferRequest> GetWarehouseOrderById(Guid id);
    public Task<WarehouseTransferRequest> CreateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest);
    public Task UpdateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest);
    public Task DeleteWarehouseOrder(Guid id);
}