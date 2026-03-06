using Warehouse.Domain.Model;

namespace Warehouse.Domain.Interfaces.Services;

public interface IProductService
{
    Task<Product?> GetProductById(Guid productId, CancellationToken cancellationToken);
    IQueryable<Product> GetAllProducts(CancellationToken cancellationToken);
    Task<Product> CreateProduct(string name, decimal price, Guid typeId, CancellationToken cancellationToken);
    Task UpdateProduct(Guid productId, string name, decimal price, Guid typeId, CancellationToken cancellationToken);
    Task DeleteProduct(Guid productId, CancellationToken cancellationToken);

    Task<ProductType?> GetProductTypeById(Guid typeId, CancellationToken cancellationToken);
    IQueryable<ProductType> GetAllProductTypes(CancellationToken cancellationToken);
    Task<ProductType> CreateProductType(string type, CancellationToken cancellationToken);
    Task UpdateProductType(Guid typeId, string type, CancellationToken cancellationToken);
    Task DeleteProductType(Guid typeId, CancellationToken cancellationToken);
}