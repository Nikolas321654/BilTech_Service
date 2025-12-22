using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IProductRepository
{
    public IQueryable<Product> GetAllProducts();
    public Task<Product?> GetProductById(Guid id);
    public Task<Product> CreateProduct(Product product);
    public Task UpdateProduct(Product product);
    public Task DeleteProduct(Guid id);
}