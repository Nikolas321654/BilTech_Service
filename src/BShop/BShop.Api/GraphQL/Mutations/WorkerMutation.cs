using BShop.Domain.Interfaces.Service;

namespace BShop.GraphQL.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class WorkerMutation
{
    public async Task<bool> RegisterWorker(
        string name,
        string login,
        string password,
        string role,
        string phoneNumber,
        [Service] IWorkerService workerService,
        CancellationToken cancellationToken)
    {
        await workerService.CreateWorkerAsync(name, login, password, role, phoneNumber, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteWorker(
        [Service] IWorkerService workerService,
        Guid workerId,
        CancellationToken cancellationToken)
    {
        await workerService.DeleteWorkerAsync(workerId, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateWorker(
        [Service] IWorkerService workerService,
        Guid workerId,
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        await workerService.UpdateWorkerAsync(workerId, phoneNumber, cancellationToken);
        return true;
    }
}