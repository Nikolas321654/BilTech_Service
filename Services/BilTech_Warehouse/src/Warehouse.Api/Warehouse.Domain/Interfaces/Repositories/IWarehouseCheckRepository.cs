using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Repositories;

public interface IWarehouseCheckRepository
{
    IQueryable<WarehouseCheck> GetAllWarehouseChecks(Guid warehouseId, CancellationToken cancellationToken);
    Task<WarehouseCheck?> GetWarehouseCheckById(Guid checkId, Guid warehouseId, CancellationToken cancellationToken);
    Task CreateWarehouseCheck(WarehouseCheck check, CancellationToken cancellationToken);
    Task UpdateWarehouseCheck(WarehouseCheck check, CancellationToken cancellationToken);
    Task DeleteWarehouseCheck(Guid checkId, Guid warehouseId, CancellationToken cancellationToken);
    Task AddItemToCheck(CheckItem item, CancellationToken cancellationToken);
    Task<CheckItem?> GetItemFromCheck(Guid checkId, Guid productId, CancellationToken cancellationToken);
    Task UpdateCheckItem(CheckItem item, CancellationToken cancellationToken);
    Task RemoveItemFromCheck(Guid checkId, Guid productId, CancellationToken cancellationToken);
}
