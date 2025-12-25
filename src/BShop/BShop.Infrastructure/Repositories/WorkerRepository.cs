using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;

namespace BShop.Infrastructure.Repositories;

public class WorkerRepository(ShopDbContext context) : IWorkerRepository
{
    public async Task<Worker?> GetWorkerById(Guid id)
    {
        return await context.Workers.FindAsync(id);
    }

    public async Task CreateWorker(Worker worker)
    {
        context.Workers.Add(worker);
        await context.SaveChangesAsync();
    }

    public async Task UpdateWorker(Worker worker)
    {
        context.Workers.Update(worker);
        await context.SaveChangesAsync();
    }

    public async Task DeleteWorker(Guid id)
    {
        var worker = await GetWorkerById(id);
        if (worker != null) context.Remove(worker);
        await context.SaveChangesAsync();
    }
}