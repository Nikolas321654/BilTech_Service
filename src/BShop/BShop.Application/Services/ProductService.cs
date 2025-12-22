using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ProductService(IProductRepository _productRepository) : IProductService
{
    private async Task<Product> ExistsProduct(Guid id)
    {
        var product = await _productRepository.GetProductById(id);
        return product ?? throw new Exception("Product not found");
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        var product = await ExistsProduct(id);
        return product;
    }

    public IQueryable<Product> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        if (product.Price <= 0) throw new Exception("Price must be greater than 0");
        if (product.ProductTypes.Count == 0) throw new Exception("Product must have at least one type");
        if (product.Name.Length < 2) throw new Exception("Product name must be at least 2 characters");

        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        return await _productRepository.CreateProduct(product);
    }

    public async Task UpdateProduct(decimal price, Guid id)
    {
        var newProduct = await ExistsProduct(id);
        if (price <= 0) throw new Exception("Price must be greater than 0");

        newProduct.Price = price;
        newProduct.UpdatedAt = DateTime.UtcNow;
        await _productRepository.UpdateProduct(newProduct);
    }

    public async Task DeleteProduct(Guid id)
    {
        await ExistsProduct(id);
        await _productRepository.DeleteProduct(id);
    }

    public async Task SoftDeleteProduct(Guid id)
    {
        var newProduct = await ExistsProduct(id);

        newProduct.IsDeleted = true;
        await _productRepository.UpdateProduct(newProduct);
    }
}