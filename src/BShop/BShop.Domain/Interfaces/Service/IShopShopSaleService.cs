using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IShopShopSaleService
{
    public Task<ShopCheck?> GetShopCheck(Guid shopId, Guid checkId);
    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId);
    public Task CreateShopCheck(Guid shopId);
    public Task UpdateShopCheck(Guid shopId, Guid checkId, decimal totalPrice);
    public Task DeleteShopCheck(Guid checkId, Guid shopId);
    public Task SoftDeleteShopCheck(Guid shopId, Guid checkId);
    public Task AddProductsToCheck(Guid shopId, Guid productId, Guid checkId, int quantity);
}