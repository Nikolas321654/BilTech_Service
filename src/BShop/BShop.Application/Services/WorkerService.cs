using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class WorkerService(IWorkerRepository workerRepository) : IWorkerService
{
    public async Task<Worker?> GetWorkerById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty) throw new BadRequestException("Id cannot be empty");

        var worker = await workerRepository.GetWorkerById(id, cancellationToken);
        return worker ?? throw new NotFoundException("Worker not found");
    }

    public IQueryable<Worker> GetAllWorkers(CancellationToken cancellationToken)
    {
        return workerRepository.GetAllWorkers(cancellationToken);
    }

    public async Task CreateWorkerAsync(string name, string login, string password, string phoneNumber, string role,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new BadRequestException("Name cannot be empty");
        if (string.IsNullOrWhiteSpace(login)) throw new BadRequestException("Login cannot be empty");
        if (string.IsNullOrWhiteSpace(password)) throw new BadRequestException("Password cannot be empty");
        if (string.IsNullOrWhiteSpace(phoneNumber)) throw new BadRequestException("Phone Number cannot be empty");
        if (string.IsNullOrWhiteSpace(role)) throw new BadRequestException("Role cannot be empty");

        if (workerRepository.GetAllWorkers(cancellationToken).Any(x => x.Login == login))
        {
            throw new BadRequestException("Worker with this login already exists");
        }

        var worker = new Worker()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Login = login,
            Password = password,
            PhoneNumber = phoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false,
        };

        await workerRepository.CreateWorker(worker, cancellationToken);
    }

    public async Task UpdateWorkerAsync(Guid workerId, string phoneNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) throw new BadRequestException("Phone Number cannot be empty");
        var worker = await GetWorkerById(workerId, cancellationToken);

        worker!.PhoneNumber = phoneNumber;
        await workerRepository.UpdateWorker(worker, cancellationToken);
    }

    public async Task DeleteWorkerAsync(Guid workerId, CancellationToken cancellationToken)
    {
        if (workerId == Guid.Empty) throw new BadRequestException("Worker id cannot be empty");
        await workerRepository.DeleteWorker(workerId, cancellationToken);
    }
}