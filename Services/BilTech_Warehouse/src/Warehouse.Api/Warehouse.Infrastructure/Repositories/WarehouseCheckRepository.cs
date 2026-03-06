using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Infrastructure.Repositories;

public class WarehouseCheckRepository(WarehouseDbContext dbContext) : IWarehouseCheckRepository
{
    public IQueryable<WarehouseCheck> GetAllWarehouseChecks(Guid warehouseId, CancellationToken cancellationToken)
    {
        return dbContext.WarehouseChecks.Where(wc => wc.WarehouseId == warehouseId && !wc.IsDeleted);
    }

    public async Task<WarehouseCheck?> GetWarehouseCheckById(Guid checkId, Guid warehouseId,
        CancellationToken cancellationToken)
    {
        return await dbContext.WarehouseChecks
            .FirstOrDefaultAsync(wc => wc.Id == checkId
                                       && wc.WarehouseId == warehouseId
                                       && !wc.IsDeleted,
                cancellationToken);
    }

    public async Task CreateWarehouseCheck(WarehouseCheck check, CancellationToken cancellationToken)
    {
        dbContext.WarehouseChecks.Add(check);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWarehouseCheck(WarehouseCheck check, CancellationToken cancellationToken)
    {
        dbContext.WarehouseChecks.Update(check);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteWarehouseCheck(Guid checkId, Guid warehouseId, CancellationToken cancellationToken)
    {
        var check = await GetWarehouseCheckById(checkId, warehouseId, cancellationToken);
        if (check != null)
        {
            check.IsDeleted = true;
            dbContext.WarehouseChecks.Update(check);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task AddItemToCheck(CheckItem item, CancellationToken cancellationToken)
    {
        await dbContext.CheckItems.AddAsync(item, cancellationToken);
    }

    public async Task<CheckItem?> GetItemFromCheck(Guid checkId, Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.CheckItems
            .FirstOrDefaultAsync(ci => ci.OrderId == checkId && ci.ProductId == productId, cancellationToken);
    }

    public Task UpdateCheckItem(CheckItem item, CancellationToken cancellationToken)
    {
        dbContext.CheckItems.Update(item);
        return Task.CompletedTask;
    }

    public async Task RemoveItemFromCheck(Guid checkId, Guid productId, CancellationToken cancellationToken)
    {
        var item = await GetItemFromCheck(checkId, productId, cancellationToken);
        if (item != null) dbContext.CheckItems.Remove(item);
    }
}