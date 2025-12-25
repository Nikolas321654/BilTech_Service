using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopStorageRepository
{
    public Task<ShopStorage?> GetProductFromShop(Guid shopId, Guid productId);
    public IQueryable<ShopStorage> GetShopAllProducts(Guid shopId);
    public Task AddProductToShop(ShopStorage shopStorage);
    public Task UpdateProductInShopStorage(ShopStorage shopStorage);
    public Task DeleteProductFromStorage(Guid shopId, Guid productId);
}