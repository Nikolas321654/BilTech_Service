using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ProductTypeRepository(ShopDbContext context) : IProductTypeRepository
{
    public IQueryable<ProductType> GetAllProductTypes(CancellationToken cancellationToken)
    {
        return context.ProductTypes.AsNoTracking();
    }

    public async Task<ProductType?> GetProductTypeById(Guid id, CancellationToken cancellationToken)
    {
        return await context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task CreateProductType(ProductType productType, CancellationToken cancellationToken)
    {
        context.Add(productType);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductType(ProductType productType, CancellationToken cancellationToken)
    {

        context.ProductTypes.Attach(productType);
        context.Entry(productType).State = EntityState.Modified;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductType(Guid id, CancellationToken cancellationToken)
    {
        var type = await context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (type != null)
        {
            context.Remove(type);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}