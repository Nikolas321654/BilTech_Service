using System.Formats.Asn1;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ShopChecksRepository(ShopDbContext context) : IShopChecksRepository
{
    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId, CancellationToken cancellationToken)
    {
        return context.ShopSales
            .Include(x => x.Shop)
            .Where(x => x.ShopId == shopId && x.IsDeleted == false)
            .AsNoTracking();
    }

    public async Task<ShopCheck?> GetShopCheckById(Guid checkId, Guid shopId, CancellationToken cancellationToken)
    {
        return await context.ShopSales
            .Include(x => x.Shop)
            .FirstOrDefaultAsync(x => x.Id == checkId && x.ShopId == shopId, cancellationToken);
    }

    public void CreateShopCheck(ShopCheck shopCheck, CancellationToken cancellationToken)
    {
        context.ShopSales.Add(shopCheck);
    }

    public void UpdateShopCheck(ShopCheck shopCheck, CancellationToken cancellationToken)
    {
        context.ShopSales.Update(shopCheck);
    }

    public async Task DeleteShopCheck(Guid checkId, Guid shopId, CancellationToken cancellationToken)
    {
        var check = await GetShopCheckById(checkId, shopId, cancellationToken);
        if (check != null) context.Remove(check);
    }

    public async Task SoftDeleteShopCheck(Guid shopId, Guid checkId, CancellationToken cancellationToken)
    {
        var check = await GetShopCheckById(checkId, shopId, cancellationToken);
        if (check != null)
        {
            check.IsDeleted = true;
            context.ShopSales.Update(check);
        }
    }

    public async Task AddProductsToCheck(SoldProduct soldProduct, CancellationToken cancellationToken)
    {
        await context.SoldProducts.AddAsync(soldProduct, cancellationToken);
    }

    public async Task<SoldProduct?> GetSoldProductFromCheck(Guid checkId, Guid productId,
        CancellationToken cancellationToken)
    {
        return await context.SoldProducts
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.ProductId == productId && x.OrderId == checkId, cancellationToken);
    }

    public async Task UpdateSoldProduct(SoldProduct soldProduct, CancellationToken cancellationToken)
    {
        context.SoldProducts.Update(soldProduct);
        await context.SaveChangesAsync(cancellationToken);
    }
}