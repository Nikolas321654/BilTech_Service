using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Repositories;

public interface IWarehouseInventoryRepository
{
    Task<WarehouseInventory?> GetProductFromWarehouse(Guid warehouseId, Guid productId, CancellationToken cancellationToken);
    IQueryable<WarehouseInventory> GetWarehouseAllProducts(Guid warehouseId, CancellationToken cancellationToken);
    Task AddProductToWarehouse(WarehouseInventory inventory, CancellationToken cancellationToken);
    Task UpdateProductInWarehouseInventory(WarehouseInventory inventory, CancellationToken cancellationToken);
    Task DeleteProductFromWarehouse(Guid warehouseId, Guid productId, CancellationToken cancellationToken);
}
