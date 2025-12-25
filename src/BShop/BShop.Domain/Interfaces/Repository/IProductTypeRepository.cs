using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IProductTypeRepository
{
    public IQueryable<ProductType> GetAllProductTypes();
    public Task<ProductType?> GetProductTypeById(Guid id);
    public Task CreateProductType(ProductType productType);
    public Task UpdateProductType(ProductType productType);
    public Task DeleteProductType(Guid id);
}