using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ShopStorageService(IShopStorageRepository shopStorageRepository, IUnitOfWork unitOfWork)
    : IShopStorageService
{
    public async Task<ShopStorage?> GetProductFromShop(Guid shopId, Guid productId, CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");
        if (productId == Guid.Empty) throw new BadRequestException("Product Id cannot be empty");

        var product = await shopStorageRepository.GetProductFromShop(shopId, productId, cancellationToken);
        return product ?? throw new NotFoundException($"Product with {productId} id, not found in shop storage");
    }

    public async Task AddProductToShop(Guid shopId, Guid productId, int quantity, decimal productPrice,
        CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");
        if (productId == Guid.Empty) throw new BadRequestException("Product Id cannot be empty");
        if (quantity <= 0) throw new BadRequestException("Quantity must be greater than 0");
        if (productPrice <= 0) throw new BadRequestException("Product price must be greater than 0");

        if (await shopStorageRepository.GetProductFromShop(shopId, productId, cancellationToken) != null)
            throw new BadRequestException($"Product with {productId} id, already exists in shop storage");

        var product = new ShopStorage()
        {
            ProductId = productId,
            ShopId = shopId,
            ProductCount = quantity,
            ProductPrice = productPrice
        };

        shopStorageRepository.AddProductToShop(product, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProductInShopStorage(Guid shopId, Guid productId, int quantity, decimal productPrice,
        CancellationToken cancellationToken)
    {
        if (productPrice <= 0) throw new BadRequestException("Product price must be greater than 0");

        var product = await GetProductFromShop(shopId, productId, cancellationToken);

        product!.ProductCount = quantity;
        product.ProductPrice = productPrice;

        shopStorageRepository.UpdateProductInShopStorage(product, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductFromStorage(Guid shopId, Guid productId, CancellationToken cancellationToken)
    {
        await GetProductFromShop(shopId, productId, cancellationToken);
        shopStorageRepository.DeleteProductFromStorage(shopId, productId, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public IQueryable<ShopStorage> GetAllProductsFromShop(Guid shopId, CancellationToken cancellationToken)
    {
        return shopId == Guid.Empty
            ? throw new BadRequestException("Shop Id cannot be empty")
            : shopStorageRepository.GetShopAllProducts(shopId, cancellationToken);
    }

    public async Task ToSellProduct(Guid shopId, Guid productId, int quantity, CancellationToken cancellationToken)
    {
        var product = await GetProductFromShop(shopId, productId, cancellationToken);
        if (product!.ProductCount < quantity) throw new BadRequestException("Not enough product in stock");

        product!.ProductCount -= quantity;
        shopStorageRepository.UpdateProductInShopStorage(product, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }
}