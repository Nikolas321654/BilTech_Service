using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty) throw new BadRequestException("Product id cannot be empty");

        var product = await productRepository.GetProductById(id, cancellationToken);
        return product ?? throw new NotFoundException($"Product with {id} id, not found");
    }

    public IQueryable<Product> GetAllProducts(CancellationToken cancellationToken)
    {
        return productRepository.GetAllProducts(cancellationToken);
    }

    public async Task CreateProduct(string name, decimal price, Guid productType, CancellationToken cancellationToken)
    {
        if (price <= 0) throw new BadRequestException("Price must be greater than 0");
        if (name.Length < 2) throw new BadRequestException("Product name must be at least 2 characters");
        if (productType == Guid.Empty) throw new BadRequestException("Product type id cannot be empty");

        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            TypeId = productType,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await productRepository.CreateProduct(product, cancellationToken);
    }

    public async Task UpdateProduct(decimal price, Guid productId, CancellationToken cancellationToken)
    {
        if (productId == Guid.Empty) throw new BadRequestException("Product id cannot be empty");
        if (price <= 0) throw new BadRequestException("Price must be greater than 0");

        var newProduct = await GetProductById(productId, cancellationToken);

        newProduct!.Price = price;
        newProduct.UpdatedAt = DateTime.UtcNow;
        await productRepository.UpdateProduct(newProduct, cancellationToken);
    }

    public async Task DeleteProduct(Guid productId, CancellationToken cancellationToken)
    {
        if (productId == Guid.Empty) throw new BadRequestException("Product id cannot be empty");

        await GetProductById(productId, cancellationToken);
        await productRepository.DeleteProduct(productId, cancellationToken);
    }

    public async Task SoftDeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty) throw new BadRequestException("Product id cannot be empty");

        var newProduct = await GetProductById(id, cancellationToken);

        newProduct!.IsDeleted = true;
        await productRepository.UpdateProduct(newProduct, cancellationToken);
    }
}