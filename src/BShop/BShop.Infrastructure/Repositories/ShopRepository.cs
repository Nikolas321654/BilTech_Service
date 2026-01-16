using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ShopRepository(ShopDbContext context) : IShopRepository
{
    public async Task<Shop?> GetShopById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Shops.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Shop?> CreateShop(Shop shop, CancellationToken cancellationToken)
    {
        context.Shops.Add(shop);
        await context.SaveChangesAsync(cancellationToken);

        return await context.Shops.Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == shop.Id, cancellationToken);
    }

    public async Task<Shop> UpdateShop(Shop shop, CancellationToken cancellationToken)
    {
        context.Shops.Attach(shop);
        context.Entry(shop).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        return shop;
    }

    public async Task DeleteShop(Guid id, CancellationToken cancellationToken)
    {
        var shop = await context.Shops.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (shop != null) context.Remove(shop);
        await context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Shop> GetAllShops(CancellationToken cancellationToken)
    {
        return context.Shops.AsNoTracking().Where(x => !x.IsDeleted).Include(x => x.Employee);
    }
}