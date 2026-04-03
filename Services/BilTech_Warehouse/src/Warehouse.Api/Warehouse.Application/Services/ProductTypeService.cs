using Warehouse.Domain.CustomExceptions;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Model;

namespace Warehouse.Application.Services;

public class ProductTypeService(IProductTypeRepository productTypeRepository) : IProductTypeService
{
    public async Task<ProductType?> GetProductTypeById(Guid typeId, CancellationToken cancellationToken)
    {
        if (typeId == Guid.Empty) throw new BadRequestException("Product type id is required.");

        var type = await productTypeRepository.GetProductTypeById(typeId, cancellationToken);
        return type ?? throw new NotFoundException("Product type not found.");
    }

    public IQueryable<ProductType> GetAllProductTypes()
    {
        return productTypeRepository.GetAllProductTypes();
    }

    public async Task<ProductType> CreateProductType(string typeName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(typeName)) throw new BadRequestException("Type is required.");
        if (typeName.Length > 50) throw new BadRequestException("Type must be less than 50 characters.");

        var type = new ProductType
        {
            Id = Guid.NewGuid(),
            Type = typeName,
            IsDeleted = false
        };

        await productTypeRepository.CreateProductType(type, cancellationToken);
        return type;
    }

    public async Task UpdateProductType(Guid typeId, string typeName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(typeName)) throw new BadRequestException("Type is required.");
        if (typeName.Length > 50) throw new BadRequestException("Type must be less than 50 characters.");
        if (typeId == Guid.Empty) throw new BadRequestException("Product type id is required.");

        var type = await GetProductTypeById(typeId, cancellationToken);
        type!.Type = typeName;

        await productTypeRepository.UpdateProductType(type, cancellationToken);
    }

    public async Task DeleteProductType(Guid typeId, CancellationToken cancellationToken)
    {
        if (typeId == Guid.Empty) throw new BadRequestException("Product type id is required.");

        await GetProductTypeById(typeId, cancellationToken);
        await productTypeRepository.DeleteProductType(typeId, cancellationToken);
    }
}