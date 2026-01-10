using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class WorkerRepository(IDbContextFactory<ShopDbContext> contextFactory) : IWorkerRepository
{
    public async Task<Worker?> GetWorkerById(Guid id, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Workers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public async Task CreateWorker(Worker worker, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        context.Workers.Add(worker);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWorker(Worker worker, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        context.Workers.Attach(worker);
        context.Entry(worker).State = EntityState.Modified;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteWorker(Guid id, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var worker = await context.Workers.FindAsync([id], cancellationToken);
        if (worker != null)
        {
            worker.IsDeleted = true;
            context.Workers.Update(worker);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public IQueryable<Worker> GetAllWorkers(CancellationToken cancellationToken)
    {
        var context = contextFactory.CreateDbContext();
        return context.Workers.AsNoTracking().Where(x => !x.IsDeleted);
    }
}