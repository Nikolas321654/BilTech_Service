using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class ShopService(IShopRepository shopRepository) : IShopService
{
    public async Task<Shop> GetShopById(Guid shopId, Guid ownerId, CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var shop = await shopRepository.GetShopById(shopId, ownerId, cancellationToken);

        return shop ?? throw new NotFoundException("Shop not found");
    }

    public IQueryable<Shop> GetAllShops(Guid ownerId, CancellationToken cancellationToken)
    {
        return shopRepository.GetAllShops(ownerId, cancellationToken);
    }

    public async Task<Shop?> CreateShop(Guid ownerId, string name, string address, string phoneNumber,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phoneNumber))
            throw new BadRequestException("Data cannot be empty");

        var shop = new Shop()
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            Name = name,
            Address = address,
            PhoneNumber = phoneNumber,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        return await shopRepository.CreateShop(shop, cancellationToken);
    }

    public async Task<Shop> UpdateShop(Guid shopId, Guid ownerId, string name, string address, string phoneNumber,
        CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phoneNumber))
            throw new BadRequestException("Data cannot be empty");

        var shop = await GetShopById(shopId, ownerId, cancellationToken);
        shop.Name = name;
        shop.Address = address;
        shop.PhoneNumber = phoneNumber;

        return await shopRepository.UpdateShop(shop, cancellationToken);
    }

    public async Task DeleteShop(Guid shopId, Guid ownerId, CancellationToken cancellationToken)
    {
        await GetShopById(shopId, ownerId, cancellationToken);
        await shopRepository.DeleteShop(shopId, ownerId, cancellationToken);
    }
}