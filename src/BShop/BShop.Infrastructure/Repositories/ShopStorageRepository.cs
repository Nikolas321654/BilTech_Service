using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ShopStorageRepository(ShopDbContext context) : IShopStorageRepository
{
    public async Task<ShopStorage?> GetProductFromShop(Guid shopId, Guid productId, CancellationToken cancellationToken)
    {
        return await context.ShopStorageInventories
            .FirstOrDefaultAsync(x =>
                x.ShopId == shopId && x.ProductId == productId, cancellationToken);
    }

    public void AddProductToShop(ShopStorage shopStorage, CancellationToken cancellationToken)
    {
        context.ShopStorageInventories.Add(shopStorage);
    }

    public void UpdateProductInShopStorage(ShopStorage shopStorage, CancellationToken cancellationToken)
    {
        context.ShopStorageInventories.Update(shopStorage);
    }

    public async Task DeleteProductFromStorage(Guid shopId, Guid productId, CancellationToken cancellationToken)
    {
        var product = await GetProductFromShop(shopId, productId, cancellationToken);
        if (product != null) context.Remove(product);
    }

    public IQueryable<ShopStorage> GetShopAllProducts(Guid shopId, CancellationToken cancellationToken)
    {
        return context.ShopStorageInventories
            .Where(x => x.ShopId == shopId)
            .AsNoTracking();
    }
}