using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopChecksRepository
{
    public IQueryable<ShopCheck> GetAllShopChecks(Guid shopId, CancellationToken cancellationToken);
    public Task<ShopCheck?> GetShopCheckById(Guid checkId, Guid shopId, CancellationToken cancellationToken);
    public void CreateShopCheck(ShopCheck shopCheck, CancellationToken cancellationToken);
    public void UpdateShopCheck(ShopCheck shopCheck, CancellationToken cancellationToken);
    public Task DeleteShopCheck(Guid checkId, Guid shopId, CancellationToken cancellationToken);
    public Task AddProductsToCheck(SoldProduct soldProduct, CancellationToken cancellationToken);
    public Task SoftDeleteShopCheck(Guid shopId, Guid checkId, CancellationToken cancellationToken);

    public Task<SoldProduct?> GetSoldProductFromCheck(Guid checkId, Guid productId,
        CancellationToken cancellationToken);

    public Task UpdateSoldProduct(SoldProduct soldProduct, CancellationToken cancellationToken);
}