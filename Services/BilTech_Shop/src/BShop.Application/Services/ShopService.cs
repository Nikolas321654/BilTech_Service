using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ShopService(IShopRepository shopRepository) : IShopService
{
    public async Task<Shop> GetShopById(Guid shopId, CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var shop = await shopRepository.GetShopById(shopId, cancellationToken);

        return shop ?? throw new NotFoundException("Shop not found");
    }

    public IQueryable<Shop> GetAllShops(CancellationToken cancellationToken)
    {
        return shopRepository.GetAllShops(cancellationToken);
    }

    public async Task<Shop?> CreateShop(Guid employeeId, string name, string address, string phoneNumber,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phoneNumber))
            throw new BadRequestException("Data cannot be empty");

        var shop = new Shop()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Address = address,
            PhoneNumber = phoneNumber,
            IsDeleted = false,
            EmployeeId = employeeId
        };

        return await shopRepository.CreateShop(shop, cancellationToken);
    }

    public async Task<Shop> UpdateShop(Guid shopId, string name, string phoneNumber,
        CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumber))
            throw new BadRequestException("Data cannot be empty");

        var shop = await GetShopById(shopId, cancellationToken);
        shop.Name = name;
        shop.PhoneNumber = phoneNumber;

        return await shopRepository.UpdateShop(shop, cancellationToken);
    }

    public async Task DeleteShop(Guid shopId, CancellationToken cancellationToken)
    {
        await GetShopById(shopId, cancellationToken);
        await shopRepository.DeleteShop(shopId, cancellationToken);
    }

    public async Task AddEmployeeToShop(Guid shopId, Guid employeeId, CancellationToken cancellationToken)
    {
        var shop = await GetShopById(shopId, cancellationToken);

        shop.EmployeeId = employeeId;
        await shopRepository.UpdateShop(shop, cancellationToken);
    }
}