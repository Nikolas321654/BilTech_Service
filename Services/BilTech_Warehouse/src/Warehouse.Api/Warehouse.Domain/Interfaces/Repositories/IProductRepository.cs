using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductById(Guid productId, CancellationToken cancellationToken);
    IQueryable<Product> GetAllProducts(CancellationToken cancellationToken);
    Task CreateProduct(Product product, CancellationToken cancellationToken);
    Task UpdateProduct(Product product, CancellationToken cancellationToken);
    Task DeleteProduct(Guid productId, CancellationToken cancellationToken);

    Task<ProductType?> GetProductTypeById(Guid typeId, CancellationToken cancellationToken);
    IQueryable<ProductType> GetAllProductTypes(CancellationToken cancellationToken);
    Task CreateProductType(ProductType type, CancellationToken cancellationToken);
    Task UpdateProductType(ProductType type, CancellationToken cancellationToken);
    Task DeleteProductType(Guid typeId, CancellationToken cancellationToken);
}