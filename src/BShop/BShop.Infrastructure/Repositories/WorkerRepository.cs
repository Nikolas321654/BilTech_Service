using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;

namespace BShop.Infrastructure.Repositories;

public class WorkerRepository(ShopDbContext context) : IWorkerRepository
{
    public async Task<Worker?> GetWorkerById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Workers.FindAsync(id, cancellationToken);
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
        var worker = await GetWorkerById(id, cancellationToken);
        if (worker != null) context.Remove(worker);
        await context.SaveChangesAsync(cancellationToken);
    }
}