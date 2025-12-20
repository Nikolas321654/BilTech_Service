using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopChecksRepository
{
    public Task<IQueryable<ShopCheck>> GetAllShopChecks();
    public Task<ShopCheck> GetShopCheckById(Guid id);
    public Task<ShopCheck> CreateShopCheck(ShopCheck shopCheck);
    public Task UpdateShopCheck(ShopCheck shopCheck);
    public Task DeleteShopCheck(Guid id);
}