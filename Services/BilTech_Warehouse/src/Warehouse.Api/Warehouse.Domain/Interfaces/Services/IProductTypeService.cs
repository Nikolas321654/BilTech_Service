using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Services;

public interface IProductTypeService
{
    public Task<ProductType?> GetProductTypeById(Guid typeId, CancellationToken cancellationToken);
    public IQueryable<ProductType> GetAllProductTypes();
    public Task<ProductType> CreateProductType(string typeName, CancellationToken cancellationToken);
    public Task UpdateProductType(Guid typeId, string typeName, CancellationToken cancellationToken);
    public Task DeleteProductType(Guid typeId, CancellationToken cancellationToken);
}