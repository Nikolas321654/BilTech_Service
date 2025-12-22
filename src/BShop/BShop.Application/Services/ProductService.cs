using BShop.Domain.CustomExepions;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private async Task<Product> ExistsProduct(Guid id)
    {
        var product = await productRepository.GetProductById(id);
        return product ?? throw new NotFoundException($"Product with {id} id, not found");
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        var product = await ExistsProduct(id);
        return product;
    }

    public IQueryable<Product> GetAllProducts()
    {
        return productRepository.GetAllProducts();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        if (product.Price <= 0) throw new BadRequestException("Price must be greater than 0");
        if (product.ProductTypes.Count == 0) throw new BadRequestException("Product must have at least one type");
        if (product.Name.Length < 2) throw new BadRequestException("Product name must be at least 2 characters");

        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        return await productRepository.CreateProduct(product);
    }

    public async Task UpdateProduct(decimal price, Guid id)
    {
        var newProduct = await ExistsProduct(id);
        if (price <= 0) throw new BadRequestException("Price must be greater than 0");

        newProduct.Price = price;
        newProduct.UpdatedAt = DateTime.UtcNow;
        await productRepository.UpdateProduct(newProduct);
    }

    public async Task DeleteProduct(Guid id)
    {
        await ExistsProduct(id);
        await productRepository.DeleteProduct(id);
    }

    public async Task SoftDeleteProduct(Guid id)
    {
        var newProduct = await ExistsProduct(id);

        newProduct.IsDeleted = true;
        await productRepository.UpdateProduct(newProduct);
    }
}