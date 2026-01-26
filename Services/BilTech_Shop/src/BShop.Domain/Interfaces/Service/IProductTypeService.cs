using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IProductTypeService
{
    public Task<ProductType?> GetProductTypeById(Guid id, CancellationToken cancellationToken);
    public IQueryable<ProductType> GetAllProductTypes(CancellationToken cancellationToken);
    public Task CreateProductType(string name, CancellationToken cancellationToken);
    public Task UpdateProductType(Guid id, string name, CancellationToken cancellationToken);
    public Task DeleteProductType(Guid id, CancellationToken cancellationToken);
}