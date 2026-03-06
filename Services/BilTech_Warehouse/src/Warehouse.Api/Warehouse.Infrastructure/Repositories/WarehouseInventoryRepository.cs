using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Infrastructure.Repositories;

public class WarehouseInventoryRepository(WarehouseDbContext dbContext) : IWarehouseInventoryRepository
{
    public async Task<WarehouseInventory?> GetProductFromWarehouse(Guid warehouseId, Guid productId,
        CancellationToken cancellationToken)
    {
        return await dbContext.WarehouseInventories
            .FirstOrDefaultAsync(wi => wi.WarehouseId == warehouseId
                                       && wi.ProductId == productId,
                cancellationToken);
    }

    public IQueryable<WarehouseInventory> GetWarehouseAllProducts(Guid warehouseId, CancellationToken cancellationToken)
    {
        return dbContext.WarehouseInventories
            .Where(wi => wi.WarehouseId == warehouseId);
    }

    public async Task AddProductToWarehouse(WarehouseInventory inventory, CancellationToken cancellationToken)
    {
        dbContext.WarehouseInventories.Add(inventory);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductInWarehouseInventory(WarehouseInventory inventory, CancellationToken cancellationToken)
    {
        dbContext.WarehouseInventories.Update(inventory);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductFromWarehouse(Guid warehouseId, Guid productId, CancellationToken cancellationToken)
    {
        var inventory = new WarehouseInventory { WarehouseId = warehouseId, ProductId = productId };
        dbContext.WarehouseInventories.Remove(inventory);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}