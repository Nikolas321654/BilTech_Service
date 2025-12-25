using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ProductTypeRepository(ShopDbContext context) : IProductTypeRepository
{
    public IQueryable<ProductType> GetAllProductTypes()
    {
        return context.ProductTypes.AsNoTracking();
    }

    public Task<ProductType?> GetProductTypeById(Guid id)
    {
        return context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateProductType(ProductType productType)
    {
        context.Add(productType);
        await context.SaveChangesAsync();
    }

    public async Task UpdateProductType(ProductType productType)
    {
        context.Update(productType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteProductType(Guid id)
    {
        var type = await GetProductTypeById(id);
        if (type != null) context.Remove(type);

        await context.SaveChangesAsync();
    }
}