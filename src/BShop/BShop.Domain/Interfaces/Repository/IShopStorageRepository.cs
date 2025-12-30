using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopStorageRepository
{
    public Task<ShopStorage?> GetProductFromShop(Guid shopId, Guid productId, CancellationToken cancellationToken);
    public IQueryable<ShopStorage> GetShopAllProducts(Guid shopId, CancellationToken cancellationToken);
    public void AddProductToShop(ShopStorage shopStorage, CancellationToken cancellationToken);
    public void UpdateProductInShopStorage(ShopStorage shopStorage, CancellationToken cancellationToken);
    public Task DeleteProductFromStorage(Guid shopId, Guid productId, CancellationToken cancellationToken);
}