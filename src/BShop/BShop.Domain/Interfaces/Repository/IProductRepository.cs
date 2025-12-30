using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IProductRepository
{
    public IQueryable<Product> GetAllProducts(CancellationToken cancellationToken);
    public Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);
    public Task CreateProduct(Product product, CancellationToken cancellationToken);
    public Task UpdateProduct(Product product, CancellationToken cancellationToken);
    public Task DeleteProduct(Guid id, CancellationToken cancellationToken);
}