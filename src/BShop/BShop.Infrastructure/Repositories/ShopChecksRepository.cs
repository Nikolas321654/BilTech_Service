using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ShopChecksRepository(ShopDbContext context) : IShopChecksRepository
{
    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId)
    {
        return context.ShopSales
            .Where(x => x.ShopId == shopId)
            .AsNoTracking();
    }

    public async Task<ShopCheck?> GetShopCheckById(Guid checkId, Guid shopId)
    {
        return await context.ShopSales
            .FirstOrDefaultAsync(x => x.Id == checkId && x.ShopId == shopId);
    }

    public async Task CreateShopCheck(ShopCheck shopCheck)
    {
        context.ShopSales.Add(shopCheck);
        await context.SaveChangesAsync();
    }

    public async Task UpdateShopCheck(ShopCheck shopCheck)
    {
        context.ShopSales.Update(shopCheck);
        await context.SaveChangesAsync();
    }

    public async Task DeleteShopCheck(Guid checkId, Guid shopId)
    {
        var chack = await GetShopCheckById(checkId, shopId);
        if (chack != null)
            context.Remove(chack);

        await context.SaveChangesAsync();
    }

    public async Task SoftDeleteShopCheck(Guid shopId, Guid checkId)
    {
        var check = await GetShopCheckById(checkId, shopId);
        if (check != null)
        {
            check.IsDeleted = true;
            await UpdateShopCheck(check);
        }
    }

    public async Task AddProductsToCheck(SoldProduct soldProduct)
    {
        await context.SoldProducts.AddAsync(soldProduct);
        await context.SaveChangesAsync();
    }
}