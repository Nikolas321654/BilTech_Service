using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IProductService
{
    public Task<Product?> GetProductById(Guid id);
    public IQueryable<Product> GetAllProducts();
    public Task<Product> CreateProduct(Product product);
    public Task UpdateProduct(decimal price, Guid id);
    public Task DeleteProduct(Guid id);
}