using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopRepository
{
    public IQueryable<Shop> GetAllShops(CancellationToken cancellationToken);
    public Task<Shop?> GetShopById(Guid id, CancellationToken cancellationToken);
    public Task CreateShop(Shop shop, CancellationToken cancellationToken);
    public Task UpdateShop(Shop shop, CancellationToken cancellationToken);
    public Task DeleteShop(Guid id, CancellationToken cancellationToken);
}