using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IWorkerRepository
{
    public Task<Worker?> GetWorkerById(Guid id);
    public Task CreateWorker(Worker worker);
    public Task UpdateWorker(Worker worker);
    public Task DeleteWorker(Guid id);
}