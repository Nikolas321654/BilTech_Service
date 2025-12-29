using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IWarehouseTransferService
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId);
    public Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId);
    public Task CreateWarehouseOrder(Guid shopId, Guid warehouseId);
    public Task UpdateWarehouseOrder(Guid shopId, Guid warehouseId, OrderStatusEnums status, DateTime? deliveryDate);
    public Task DeleteWarehouseOrder(Guid shopId, Guid orderId);
    public Task AddProductToWarehouseOrder(Guid shopId, Guid orderId, Guid productId, int quantity);
}