using Warehouse.Domain.Interfaces;

namespace Warehouse.Infrastructure;

public class UnitOfWork(WarehouseDbContext dbContext) : IUnitOfWork
{
    public async Task BeginTransactionAsync()
    {
        await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (dbContext.Database.CurrentTransaction != null)
        {
            await dbContext.Database.CommitTransactionAsync();
        }
    }

    public async Task RollbackAsync()
    {
        if (dbContext.Database.CurrentTransaction != null)
        {
            await dbContext.Database.RollbackTransactionAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}