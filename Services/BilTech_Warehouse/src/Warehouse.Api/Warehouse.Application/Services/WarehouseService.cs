using Warehouse.Domain.CustomExceptions;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Model;

namespace Warehouse.Application.Services;

public class WarehouseService(
    IWarehouseRepository warehouseRepository) : IWarehouseService
{
    public async Task<Domain.Model.Warehouse?> GetWarehouseById(Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.GetWarehouseById(warehouseId, cancellationToken);
        return warehouse ?? throw new NotFoundException("Warehouse not found.");
    }

    public IQueryable<Domain.Model.Warehouse> GetAllWarehouses(Guid ownerId, CancellationToken cancellationToken)
    {
        return warehouseRepository.GetAllWarehouses(ownerId, cancellationToken);
    }

    public async Task<Domain.Model.Warehouse> CreateWarehouse(Guid ownerId, string address, string phoneNumber,
        CancellationToken cancellationToken)
    {
        var warehouse = new Domain.Model.Warehouse
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            Address = address,
            PhoneNumber = phoneNumber,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await warehouseRepository.CreateWarehouse(warehouse, cancellationToken);
        return warehouse;
    }

    public async Task UpdateWarehouse(Guid ownerId, Guid warehouseId, string address, string phoneNumber,
        CancellationToken cancellationToken)
    {
        var warehouse = await GetWarehouseById(warehouseId, cancellationToken);

        warehouse!.Address = address;
        warehouse.PhoneNumber = phoneNumber;

        await warehouseRepository.UpdateWarehouse(ownerId, warehouse, cancellationToken);
    }

    public async Task DeleteWarehouse(Guid ownerId, Guid warehouseId, CancellationToken cancellationToken)
    {
        await GetWarehouseById(warehouseId, cancellationToken);
        await warehouseRepository.DeleteWarehouse(ownerId, warehouseId, cancellationToken);
    }
}