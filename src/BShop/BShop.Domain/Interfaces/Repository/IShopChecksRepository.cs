using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopChecksRepository
{
    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId);
    public Task<ShopCheck?> GetShopCheckById(Guid checkId, Guid shopId);
    public Task CreateShopCheck(ShopCheck shopCheck);
    public Task UpdateShopCheck(ShopCheck shopCheck);
    public Task DeleteShopCheck(Guid checkId, Guid shopId);
}