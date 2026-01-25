using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IWorkerRepository
{
    public Task<Worker?> GetWorkerById(Guid id, CancellationToken cancellationToken);
    public Task CreateWorker(Worker worker, CancellationToken cancellationToken);
    public Task UpdateWorker(Worker worker, CancellationToken cancellationToken);
    public Task DeleteWorker(Guid id, CancellationToken cancellationToken);
    public IQueryable<Worker> GetAllWorkers(CancellationToken cancellationToken);
    public Task<Worker?> GetWorkerByLogin(string login, CancellationToken cancellationToken);
}