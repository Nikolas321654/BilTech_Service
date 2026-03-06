using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Repositories;

public interface IWarehouseRepository
{
    Task<Model.Warehouse?> GetWarehouseById(Guid warehouseId, CancellationToken cancellationToken);
    IQueryable<Model.Warehouse> GetAllWarehouses(Guid ownerId, CancellationToken cancellationToken);
    Task CreateWarehouse(Model.Warehouse warehouse, CancellationToken cancellationToken);
    Task UpdateWarehouse(Guid ownerId, Model.Warehouse warehouse, CancellationToken cancellationToken);
    Task DeleteWarehouse(Guid ownerId, Guid warehouseId, CancellationToken cancellationToken);
}