using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IShopSaleService
{
    public Task<ShopCheck?> GetShopCheck(Guid shopId, Guid checkId, CancellationToken cancellationToken);
    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId, CancellationToken cancellationToken);
    public Task<ShopCheck> CreateShopCheck(Guid shopId, CancellationToken cancellationToken);
    public Task DeleteShopCheck(Guid checkId, Guid shopId, CancellationToken cancellationToken);
    public Task SoftDeleteShopCheck(Guid shopId, Guid checkId, CancellationToken cancellationToken);

    public Task AddProductsToCheck(Guid shopId, Guid productId, Guid checkId, int quantity,
        CancellationToken cancellationToken);

    public Task UpdateShopCheck(Guid shopId, Guid checkId, int productsCount,
        CancellationToken cancellationToken);
}