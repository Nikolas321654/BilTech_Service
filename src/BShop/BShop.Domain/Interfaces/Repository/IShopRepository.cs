using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Repository;

public interface IShopRepository
{
    public Task<Shop> GetShopById(Guid id);
    public Task<Shop> CreateShop(Shop shop);
    public Task UpdateShop(Shop shop);
    public Task DeleteShop(Guid id);
    public Task<IQueryable<Shop>> GetAllShops();
}