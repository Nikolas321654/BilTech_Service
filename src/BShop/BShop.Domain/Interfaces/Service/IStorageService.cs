using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IStorageService
{
    public Task<ShopStorage?> GetProductFromShop(Guid shopId, Guid productId, CancellationToken cancellationToken);

    public Task AddProductToShop(Guid shopId, Guid productId, int quantity, decimal productPrice,
        CancellationToken cancellationToken);

    public Task UpdateProductInShopStorage(Guid shopId, Guid productId, int quantity, decimal productPrice,
        CancellationToken cancellationToken);

    public Task DeleteProductFromStorage(Guid shopId, Guid productId, CancellationToken cancellationToken);
    public IQueryable<ShopStorage> GetAllProductsFromShop(Guid shopId, CancellationToken cancellationToken);
    public Task ToSellProduct(Guid shopId, Guid productId, int quantity, CancellationToken cancellationToken);
}