using Warehouse.Domain.CustomExceptions;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Model;

namespace Warehouse.Application.Services;

public class ProductService(
    IProductRepository productRepository) : IProductService
{
    public async Task<Product?> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        if(productId == Guid.Empty) throw new BadRequestException("Product id is required.");
        var product = await productRepository.GetProductById(productId, cancellationToken);
        return product ?? throw new NotFoundException("Product not found.");
    }

    public IQueryable<Product> GetAllProducts(CancellationToken cancellationToken)
    {
        return productRepository.GetAllProducts(cancellationToken);
    }

    public async Task<Product> CreateProduct(string name, decimal price, Guid typeId, CancellationToken cancellationToken)
    {
        if(typeId == Guid.Empty) throw new BadRequestException("Product type is required.");
        if(price <= 0) throw new BadRequestException("Price must be greater than 0.");
        if(string.IsNullOrWhiteSpace(name)) throw new BadRequestException("Name is required.");
        
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            ProductTypeId = typeId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await productRepository.CreateProduct(product, cancellationToken);
        return product;
    }

    public async Task UpdateProduct(Guid productId, string name, decimal price, Guid typeId, CancellationToken cancellationToken)
    {
        if(typeId == Guid.Empty) throw new BadRequestException("Product type is required.");
        if(price <= 0) throw new BadRequestException("Price must be greater than 0.");
        if(string.IsNullOrWhiteSpace(name)) throw new BadRequestException("Name is required.");
        if(productId == Guid.Empty) throw new BadRequestException("Product id is required.");
        
        var product = await GetProductById(productId, cancellationToken);
        
        product!.Name = name;
        product.Price = price;
        product.ProductTypeId = typeId;
        product.UpdatedAt = DateTime.UtcNow;

        await productRepository.UpdateProduct(product, cancellationToken);
    }

    public async Task DeleteProduct(Guid productId, CancellationToken cancellationToken)
    {
        if(productId == Guid.Empty) throw new BadRequestException("Product id is required.");
        
        await GetProductById(productId, cancellationToken);
        await productRepository.DeleteProduct(productId, cancellationToken);
    }

    public async Task<ProductType?> GetProductTypeById(Guid typeId, CancellationToken cancellationToken)
    {
        if(typeId == Guid.Empty) throw new BadRequestException("Product type id is required.");
        
        var type = await productRepository.GetProductTypeById(typeId, cancellationToken);
        return type ?? throw new NotFoundException("Product type not found.");
    }

    public IQueryable<ProductType> GetAllProductTypes(CancellationToken cancellationToken)
    {
        return productRepository.GetAllProductTypes(cancellationToken);
    }

    public async Task<ProductType> CreateProductType(string typeName, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(typeName)) throw new BadRequestException("Type is required.");
        if(typeName.Length > 50) throw new BadRequestException("Type must be less than 50 characters.");
        
        var type = new ProductType
        {
            Id = Guid.NewGuid(),
            Type = typeName,
            IsDeleted = false
        };

        await productRepository.CreateProductType(type, cancellationToken);
        return type;
    }

    public async Task UpdateProductType(Guid typeId, string typeName, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(typeName)) throw new BadRequestException("Type is required.");
        if(typeName.Length > 50) throw new BadRequestException("Type must be less than 50 characters.");
        if(typeId == Guid.Empty) throw new BadRequestException("Product type id is required.");
        
        var type = await GetProductTypeById(typeId, cancellationToken);
        type!.Type = typeName;

        await productRepository.UpdateProductType(type, cancellationToken);
    }

    public async Task DeleteProductType(Guid typeId, CancellationToken cancellationToken)
    {
        if(typeId == Guid.Empty) throw new BadRequestException("Product type id is required.");
        
        await GetProductTypeById(typeId, cancellationToken);
        await productRepository.DeleteProductType(typeId, cancellationToken);
    }
}