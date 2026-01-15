using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Service;

public interface IShopService
{
    public Task<Shop> GetShopById(Guid shopId, CancellationToken cancellationToken);

    public Task<Shop?> CreateShop(Guid employeeId, string name, string address, string phoneNumber,
        CancellationToken cancellationToken);

    public Task<Shop> UpdateShop(Guid shopId, string address, string phoneNumber, CancellationToken cancellationToken);
    public Task DeleteShop(Guid shopId, CancellationToken cancellationToken);
    public Task AddEmployeeToShop(Guid shopId, Guid employeeId, CancellationToken cancellationToken);
    public IQueryable<Shop> GetAllShops(CancellationToken cancellationToken);
}