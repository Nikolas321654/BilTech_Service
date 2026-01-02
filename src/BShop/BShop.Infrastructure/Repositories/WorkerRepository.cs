using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class WorkerRepository(ShopDbContext context) : IWorkerRepository
{
    public async Task<Worker?> GetWorkerById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Workers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }

    public async Task CreateWorker(Worker worker, CancellationToken cancellationToken)
    {
        context.Workers.Add(worker);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWorker(Worker worker, CancellationToken cancellationToken)
    {
        context.Workers.Update(worker);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteWorker(Guid id, CancellationToken cancellationToken)
    {
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
        return context.Workers.AsNoTracking().Where(x => !x.IsDeleted);
    }
}