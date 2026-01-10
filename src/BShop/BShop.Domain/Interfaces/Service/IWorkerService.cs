using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IWorkerService
{
    public Task<Worker?> GetWorkerById(Guid id, CancellationToken cancellationToken);
    public IQueryable<Worker> GetAllWorkers(CancellationToken cancellationToken);

    public Task CreateWorkerAsync(string name, string login, string password, string role,
        string phoneNumber,
        CancellationToken cancellationToken);

    public Task UpdateWorkerAsync(Guid workerId, string phoneNumber, CancellationToken cancellationToken);
    public Task DeleteWorkerAsync(Guid workerId, CancellationToken cancellationToken);
}