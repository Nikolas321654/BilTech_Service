using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ShopSaleService(
    IShopChecksRepository shopChecksRepository,
    IShopStorageRepository shopStorageRepository,
    IUnitOfWork unitOfWork,
    IStorageService storageService
) : IShopSaleService
{
    public async Task<ShopCheck?> GetShopCheck(Guid shopId, Guid checkId, CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");
        if (checkId == Guid.Empty) throw new BadRequestException("Check Id cannot be empty");

        var check = await shopChecksRepository.GetShopCheckById(checkId, shopId, cancellationToken);
        return check ?? throw new NotFoundException($"Shop check with {checkId} id, not found");
    }

    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId, CancellationToken cancellationToken)
    {
        return shopId == Guid.Empty
            ? throw new BadRequestException("Shop Id cannot be empty")
            : shopChecksRepository.GetAllShopChecks(shopId, cancellationToken);
    }

    public async Task CreateShopCheck(Guid shopId, CancellationToken cancellationToken)
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

        shopChecksRepository.CreateShopCheck(shopCheck, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateShopCheck(Guid shopId, Guid checkId, decimal totalPrice,
        CancellationToken cancellationToken)
    {
        var shopCheck = await GetShopCheck(shopId, checkId, cancellationToken);

        shopCheck!.TotalPrice = totalPrice;
        shopCheck.UpdatedAt = DateTime.UtcNow;

        shopChecksRepository.UpdateShopCheck(shopCheck, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteShopCheck(Guid checkId, Guid shopId, CancellationToken cancellationToken)
    {
        await GetShopCheck(shopId, checkId, cancellationToken);
        await shopChecksRepository.DeleteShopCheck(checkId, shopId, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task SoftDeleteShopCheck(Guid shopId, Guid checkId, CancellationToken cancellationToken)
    {
        var check = await GetShopCheck(shopId, checkId, cancellationToken);

        check!.IsDeleted = true;
        shopChecksRepository.UpdateShopCheck(check, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddProductsToCheck(Guid shopId, Guid productId, Guid checkId, int quantity,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var product = await shopStorageRepository.GetProductFromShop(shopId, productId, cancellationToken);
            if (product == null) throw new NotFoundException($"Product with {productId} id, not found");
            if (product.ProductCount < quantity)
                throw new BadRequestException($"Not enough product in stock, available: {product.ProductCount}");

            var check = await shopChecksRepository.GetShopCheckById(checkId, shopId, cancellationToken);
            if (check == null) throw new NotFoundException($"Shop check with {checkId} id, not found");
            if (check.IsDeleted) throw new BadRequestException($"Shop check with {checkId} id, is deleted");

            product.ProductCount -= quantity;

            var soldProduct = new SoldProduct()
            {
                OrderId = checkId,
                ProductId = productId,
                ProductCount = quantity,
                TotalPrice = product.ProductPrice * quantity
            };

            check.TotalPrice += product.ProductPrice * quantity;

            await shopChecksRepository.AddProductsToCheck(soldProduct, cancellationToken);
            shopChecksRepository.UpdateShopCheck(check, cancellationToken);
            shopStorageRepository.UpdateProductInShopStorage(product, cancellationToken);
            await storageService.ToSellProduct(shopId, productId, quantity, cancellationToken);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}