using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ProductTypeService(IProductTypeRepository productTypeRepository) : IProductTypeService
{
    public Task<ProductType?> GetProductTypeById(Guid id, CancellationToken cancellationToken)
    {
        return id == Guid.Empty
            ? throw new BadRequestException("Product type id cannot be empty")
            : productTypeRepository.GetProductTypeById(id, cancellationToken);
    }

    public IQueryable<ProductType> GetAllProductTypes(CancellationToken cancellationToken)
    {
        return productTypeRepository.GetAllProductTypes(cancellationToken);
    }

    public Task CreateProductType(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name)) throw new BadRequestException("Product type name cannot be empty");

        var productType = new ProductType()
        {
            Id = Guid.NewGuid(),
            Type = name,
            IsDeleted = false
        };

        return productTypeRepository.CreateProductType(productType, cancellationToken);
    }

    public Task UpdateProductType(Guid id, string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name)) throw new BadRequestException("Product type name cannot be empty");
        if (id == Guid.Empty) throw new BadRequestException("Product type id cannot be empty");

        var productType = new ProductType() { Id = id, Type = name };
        return productTypeRepository.UpdateProductType(productType, cancellationToken);
    }

    public Task DeleteProductType(Guid id, CancellationToken cancellationToken)
    {
        return id == Guid.Empty
            ? throw new BadRequestException("Product type id cannot be empty")
            : productTypeRepository.DeleteProductType(id, cancellationToken);
    }
}