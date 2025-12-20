using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopStorageRepository
{
    public Task<ShopStorage> GetShopStorageById(Guid id);
    public Task<ShopStorage> CreateShopStorage(ShopStorage shopStorage);
    public Task UpdateShopStorage(ShopStorage shopStorage);
    public Task DeleteShopStorage(Guid id);
    public Task<IQueryable<ShopStorage>> GetAllShopStorages();
    public Task<IQueryable<Product>> GetShopAllProducts(Guid id);
}