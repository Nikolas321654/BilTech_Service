using Warehouse.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Infrastructure.Repositories;

public class WarehouseRepository(WarehouseDbContext dbContext) : IWarehouseRepository
{
    public async Task<Domain.Model.Warehouse?> GetWarehouseById(Guid warehouseId, CancellationToken cancellationToken)
    {
        return await dbContext.Warehouses
            .FirstOrDefaultAsync(w => w.Id == warehouseId && !w.IsDeleted, cancellationToken);
    }

    public IQueryable<Domain.Model.Warehouse> GetAllWarehouses(Guid ownerId, CancellationToken cancellationToken)
    {
        return dbContext.Warehouses.Where(w => !w.IsDeleted && w.OwnerId == ownerId);
    }

    public async Task CreateWarehouse(Domain.Model.Warehouse warehouse, CancellationToken cancellationToken)
    {
        dbContext.Warehouses.Add(warehouse);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWarehouse(Guid ownerId, Domain.Model.Warehouse warehouse,
        CancellationToken cancellationToken)
    {
        if (warehouse.OwnerId != ownerId)
        {
            dbContext.Warehouses.Update(warehouse);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteWarehouse(Guid ownerId, Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse = await GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse != null && warehouse.OwnerId == ownerId)
        {
            warehouse.IsDeleted = true;
            dbContext.Warehouses.Update(warehouse);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}