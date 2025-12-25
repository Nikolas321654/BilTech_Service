using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ShopStorageRepository(ShopDbContext context) : IShopStorageRepository
{
    public async Task<ShopStorage?> GetProductFromShop(Guid shopId, Guid productId)
    {
        return await context.ShopStorageInventories
            .FirstOrDefaultAsync(x =>
                x.ShopId == shopId && x.ProductId == productId);
    }

    public async Task AddProductToShop(ShopStorage shopStorage)
    {
        context.Add(shopStorage);
        await context.SaveChangesAsync();
    }

    public async Task UpdateProductInShopStorage(ShopStorage shopStorage)
    {
        context.ShopStorageInventories.Update(shopStorage);
        await context.SaveChangesAsync();
    }

    public async Task DeleteProductFromStorage(Guid shopId, Guid productId)
    {
        var product = await GetProductFromShop(shopId, productId);
        if (product != null) context.Remove(product);

        await context.SaveChangesAsync();
    }

    public IQueryable<ShopStorage> GetShopAllProducts(Guid shopId)
    {
        return context.ShopStorageInventories
            .Where(x => x.ShopId == shopId)
            .AsNoTracking();
    }
}