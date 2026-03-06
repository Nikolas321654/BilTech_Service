using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Services;

public interface IWarehouseService
{
    Task<Model.Warehouse?> GetWarehouseById(Guid warehouseId, CancellationToken cancellationToken);
    IQueryable<Model.Warehouse> GetAllWarehouses(Guid ownerId, CancellationToken cancellationToken);

    Task<Model.Warehouse> CreateWarehouse(Guid ownerId, string address, string phoneNumber,
        CancellationToken cancellationToken);

    Task UpdateWarehouse(Guid ownerId, Guid warehouseId, string address, string phoneNumber,
        CancellationToken cancellationToken);

    Task DeleteWarehouse(Guid ownerId, Guid warehouseId, CancellationToken cancellationToken);
}