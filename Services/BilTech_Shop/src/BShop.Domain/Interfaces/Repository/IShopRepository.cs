using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopRepository
{
    public IQueryable<Shop> GetAllShops(Guid ownerId, CancellationToken cancellationToken);
    public Task<Shop?> GetShopById(Guid id, Guid ownerId, CancellationToken cancellationToken);
    public Task<Shop?> CreateShop(Shop shop, CancellationToken cancellationToken);
    public Task<Shop> UpdateShop(Shop shop, CancellationToken cancellationToken);
    public Task DeleteShop(Guid id, Guid ownerId, CancellationToken cancellationToken);
}