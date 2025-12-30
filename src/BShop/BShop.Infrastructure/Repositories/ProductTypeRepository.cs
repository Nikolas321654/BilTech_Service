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

    public Task<ProductType?> GetProductTypeById(Guid id, CancellationToken cancellationToken)
    {
        return context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task CreateProductType(ProductType productType, CancellationToken cancellationToken)
    {
        context.Add(productType);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductType(ProductType productType, CancellationToken cancellationToken)
    {
        context.Update(productType);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductType(Guid id, CancellationToken cancellationToken)
    {
        var type = await GetProductTypeById(id, cancellationToken);
        if (type != null) context.Remove(type);

        await context.SaveChangesAsync(cancellationToken);
    }
}