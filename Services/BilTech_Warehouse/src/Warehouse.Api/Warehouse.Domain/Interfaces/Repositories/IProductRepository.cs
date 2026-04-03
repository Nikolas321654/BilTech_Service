using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductById(Guid productId, CancellationToken cancellationToken);
    IQueryable<Product> GetAllProducts(CancellationToken cancellationToken);
    Task CreateProduct(Product product, CancellationToken cancellationToken);
    Task UpdateProduct(Product product, CancellationToken cancellationToken);
    Task DeleteProduct(Guid productId, CancellationToken cancellationToken);
}