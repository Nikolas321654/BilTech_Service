using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ShopRepository(ShopDbContext context) : IShopRepository
{
    public async Task<Shop?> GetShopById(Guid id, Guid ownerId, CancellationToken cancellationToken)
    {
        return await context.Shops.FirstOrDefaultAsync(x => x.Id == id && x.OwnerId == ownerId && !x.IsDeleted, cancellationToken);
    }


    public async Task<Shop?> CreateShop(Shop shop, CancellationToken cancellationToken)
    {
        context.Shops.Add(shop);
        await context.SaveChangesAsync(cancellationToken);

        return await context.Shops.FirstOrDefaultAsync(x => x.Id == shop.Id, cancellationToken);
    }

    public async Task<Shop> UpdateShop(Shop shop, CancellationToken cancellationToken)
    {
        context.Shops.Attach(shop);
        context.Entry(shop).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        return shop;
    }

    public async Task DeleteShop(Guid id, Guid ownerId, CancellationToken cancellationToken)
    {
        var shop = await context.Shops.FirstOrDefaultAsync(x => x.Id == id && x.OwnerId == ownerId, cancellationToken);
        if (shop != null)
        {
            shop.IsDeleted = true;
            context.Shops.Update(shop);
        }
        await context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Shop> GetAllShops(Guid ownerId, CancellationToken cancellationToken)
    {
        return context.Shops.AsNoTracking().Where(x => !x.IsDeleted && x.OwnerId == ownerId);
    }
}