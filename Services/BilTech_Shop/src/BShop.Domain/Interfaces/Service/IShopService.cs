using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IShopService
{
    public Task<Shop> GetShopById(Guid shopId, Guid ownerId, CancellationToken cancellationToken);

    public Task<Shop?> CreateShop(Guid ownerId, string name, string address, string phoneNumber,
        CancellationToken cancellationToken);

    public Task<Shop> UpdateShop(Guid shopId, Guid ownerId, string name, string address, string phoneNumber,
        CancellationToken cancellationToken);

    public Task DeleteShop(Guid shopId, Guid ownerId, CancellationToken cancellationToken);
    public IQueryable<Shop> GetAllShops(Guid ownerId, CancellationToken cancellationToken);
}