using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Services;

public interface IWarehouseInventoryService
{
    Task<WarehouseInventory?> GetProductFromWarehouse(Guid warehouseId, Guid productId, CancellationToken cancellationToken);
    Task AddProductToWarehouse(Guid warehouseId, Guid productId, int quantity, decimal productPrice, CancellationToken cancellationToken);
    Task UpdateProductInWarehouseInventory(Guid warehouseId, Guid productId, int quantity, decimal productPrice, CancellationToken cancellationToken);
    Task DeleteProductFromWarehouse(Guid warehouseId, Guid productId, CancellationToken cancellationToken);
    IQueryable<WarehouseInventory> GetAllProductsFromWarehouse(Guid warehouseId, CancellationToken cancellationToken);
    Task DecreaseProductQuantity(Guid warehouseId, Guid productId, int quantity, CancellationToken cancellationToken);
}
