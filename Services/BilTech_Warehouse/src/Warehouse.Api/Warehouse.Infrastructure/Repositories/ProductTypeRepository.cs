using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure.Repositories;

public class ProductTypeRepository(WarehouseDbContext dbContext) : IProductTypeRepository
{
    public async Task<ProductType?> GetProductTypeById(Guid typeId, CancellationToken cancellationToken)
    {
        return await dbContext.ProductTypes
            .FirstOrDefaultAsync(pt => pt.Id == typeId && !pt.IsDeleted, cancellationToken);
    }

    public IQueryable<ProductType> GetAllProductTypes()
    {
        return dbContext.ProductTypes.Where(pt => !pt.IsDeleted);
    }

    public async Task CreateProductType(ProductType type, CancellationToken cancellationToken)
    {
        dbContext.ProductTypes.Add(type);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductType(ProductType type, CancellationToken cancellationToken)
    {
        dbContext.ProductTypes.Update(type);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductType(Guid typeId, CancellationToken cancellationToken)
    {
        var type = await GetProductTypeById(typeId, cancellationToken);
        if (type != null)
        {
            type.IsDeleted = true;
            dbContext.ProductTypes.Update(type);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}