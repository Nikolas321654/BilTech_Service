using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Repositories;

public interface IProductTypeRepository
{
    public Task<ProductType?> GetProductTypeById(Guid typeId, CancellationToken cancellationToken);
    public IQueryable<ProductType> GetAllProductTypes();
    public Task CreateProductType(ProductType type, CancellationToken cancellationToken);
    public Task UpdateProductType(ProductType type, CancellationToken cancellationToken);
    public Task DeleteProductType(Guid typeId, CancellationToken cancellationToken);
}