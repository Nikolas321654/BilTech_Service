using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IProductTypeRepository
{
    public IQueryable<ProductType> GetAllProductTypes(CancellationToken cancellationToken);
    public Task<ProductType?> GetProductTypeById(Guid id, CancellationToken cancellationToken);
    public Task CreateProductType(ProductType productType, CancellationToken cancellationToken);
    public Task UpdateProductType(ProductType productType, CancellationToken cancellationToken);
    public Task DeleteProductType(Guid id, CancellationToken cancellationToken);
}