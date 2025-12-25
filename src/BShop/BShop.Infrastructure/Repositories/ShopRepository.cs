using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ShopRepository(ShopDbContext context) : IShopRepository
{
    public async Task<Shop?> GetShopById(Guid id)
    {
        return await context.Shops.FindAsync(id);
    }

    public async Task CreateShop(Shop shop)
    {
        context.Shops.Add(shop);
        await context.SaveChangesAsync();
    }

    public async Task UpdateShop(Shop shop)
    {
        context.Shops.Update(shop);
        await context.SaveChangesAsync();
    }

    public async Task DeleteShop(Guid id)
    {
        var shop = await GetShopById(id);
        if (shop != null) context.Remove(shop);
        await context.SaveChangesAsync();
    }

    public IQueryable<Shop> GetAllShops()
    {
        return context.Shops.AsNoTracking();
    }
}