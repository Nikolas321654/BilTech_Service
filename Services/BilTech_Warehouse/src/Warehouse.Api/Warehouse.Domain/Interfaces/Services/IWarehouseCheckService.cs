using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Services;

public interface IWarehouseCheckService
{
    Task<WarehouseCheck> CreateWarehouseCheck(Guid warehouseId, CancellationToken cancellationToken);
    Task AddItemToWarehouseCheck(Guid checkId, Guid warehouseId, Guid productId, int quantity, CancellationToken cancellationToken);
    Task RemoveItemFromWarehouseCheck(Guid checkId, Guid warehouseId, Guid productId, CancellationToken cancellationToken);
    Task DeleteWarehouseCheck(Guid checkId, Guid warehouseId, CancellationToken cancellationToken);
    Task<WarehouseCheck?> GetWarehouseCheckById(Guid checkId, Guid warehouseId, CancellationToken cancellationToken);
    IQueryable<WarehouseCheck> GetAllWarehouseChecks(Guid warehouseId, CancellationToken cancellationToken);
}
