using BShop.Domain.Interfaces.Service;

namespace BShop.GraphQL.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class ShopStorageMutation
{
    public async Task<bool> AddProductToShopStorage([Service] IShopStorageService shopStorageService, Guid shopId,
        Guid productId, int productCount, decimal productPrice, CancellationToken cancellationToken)
    {
        await shopStorageService.AddProductToShop(shopId, productId, productCount, productPrice, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteProductFromStorage([Service] IShopStorageService shopStorageService, Guid shopId,
        Guid productId, CancellationToken cancellationToken)
    {
        await shopStorageService.DeleteProductFromStorage(shopId, productId, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateProductInStorage([Service] IShopStorageService shopStorageService, Guid shopId,
        Guid productId, int productCount, decimal productPrice, CancellationToken cancellationToken)
    {
        await shopStorageService.UpdateProductInShopStorage(shopId, productId, productCount, productPrice,
            cancellationToken);
        return true;
    }

    public async Task<bool> ToSellProduct([Service] IShopStorageService shopStorageService, Guid shopId, Guid productId,
        int quantity, CancellationToken cancellationToken)
    {
        await shopStorageService.ToSellProduct(shopId, productId, quantity, cancellationToken);
        return true;
    }
}