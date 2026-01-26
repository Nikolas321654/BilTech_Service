using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IProductService
{
    public Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);
    public IQueryable<Product> GetAllProducts(CancellationToken cancellationToken);
    public Task CreateProduct(string name, decimal price, Guid productType, CancellationToken cancellationToken);
    public Task UpdateProduct(decimal price, Guid id, CancellationToken cancellationToken);
    public Task DeleteProduct(Guid id, CancellationToken cancellationToken);
    public Task SoftDeleteProduct(Guid id, CancellationToken cancellationToken);
}