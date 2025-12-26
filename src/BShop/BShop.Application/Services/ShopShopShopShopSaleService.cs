using BShop.Domain.CustomExepions;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;
using BShop.Infrastructure;

namespace BShop.Application.Services;

public class ShopShopShopShopSaleService(
    IShopChecksRepository shopChecksRepository,
    IShopStorageRepository shopStorageRepository,
    ShopDbContext context)
    : IShopShopSaleService
{
    public async Task<ShopCheck?> GetShopCheck(Guid shopId, Guid checkId)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");
        if (checkId == Guid.Empty) throw new BadRequestException("Check Id cannot be empty");

        var check = await shopChecksRepository.GetShopCheckById(checkId, shopId);
        return check ?? throw new NotFoundException($"Shop check with {checkId} id, not found");
    }

    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId)
    {
        return shopId == Guid.Empty
            ? throw new BadRequestException("Shop Id cannot be empty")
            : shopChecksRepository.GetAllShopChecks(shopId);
    }

    public async Task CreateShopCheck(Guid shopId)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");

        var shopCheck = new ShopCheck()
        {
            Id = Guid.NewGuid(),
            ShopId = shopId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false,
            TotalPrice = 0
        };

        await shopChecksRepository.CreateShopCheck(shopCheck);
    }

    public async Task UpdateShopCheck(Guid shopId, Guid checkId, decimal totalPrice)
    {
        var shopCheck = await GetShopCheck(shopId, checkId);

        shopCheck!.TotalPrice = totalPrice;
        shopCheck.UpdatedAt = DateTime.UtcNow;
        await shopChecksRepository.UpdateShopCheck(shopCheck);
    }

    public async Task DeleteShopCheck(Guid checkId, Guid shopId)
    {
        var shopCheck = await GetShopCheck(shopId, checkId);
        await shopChecksRepository.DeleteShopCheck(checkId, shopId);
    }

    public async Task SoftDeleteShopCheck(Guid shopId, Guid checkId)
    {
        var check = await GetShopCheck(shopId, checkId);

        check!.IsDeleted = true;
        await shopChecksRepository.UpdateShopCheck(check);
    }

    public async Task AddProductsToCheck(Guid shopId, Guid productId, Guid checkId, int quantity)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var product = await shopStorageRepository.GetProductFromShop(shopId, productId);
            if (product == null) throw new NotFoundException($"Product with {productId} id, not found");
            if (product.ProductCount < quantity)
                throw new BadRequestException($"Not enough product in stock, available: {product.ProductCount}");

            var check = await shopChecksRepository.GetShopCheckById(checkId, shopId);
            if (check == null) throw new NotFoundException($"Shop check with {checkId} id, not found");
            product.ProductCount -= quantity;

            var soldProduct = new SoldProduct()
            {
                OrderId = checkId,
                ProductId = productId,
                ProductCount = quantity,
                TotalPrice = product.ProductPrice * quantity
            };

            check.TotalPrice += product.ProductPrice * quantity;

            await shopChecksRepository.AddProductsToCheck(soldProduct);
            await shopChecksRepository.UpdateShopCheck(check);
            await shopStorageRepository.UpdateProductInShopStorage(product);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}