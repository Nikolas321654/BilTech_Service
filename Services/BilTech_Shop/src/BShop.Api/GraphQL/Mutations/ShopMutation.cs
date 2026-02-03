using System.Data.Common;
using AutoMapper;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Models;

namespace BShop.GraphQL.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class ShopMutation
{
    public async Task<ShopApi> RegisterShop([Service] IShopService shopService,
        [Service] IMapper mapper,
        string shopName,
        string shopAddress,
        string shopPhoneNumber,
        CancellationToken cancellationToken)
    {
        var shop = await shopService.CreateShop(shopName, shopAddress, shopPhoneNumber,
            cancellationToken);

        return mapper.Map<ShopApi>(shop);
    }

    public async Task<ShopApi> UpdateShop([Service] IShopService shopService,
        [Service] IMapper mapper,
        Guid shopId,
        string shopAddress,
        string shopPhoneNumber,
        CancellationToken cancellationToken)
    {
        var shop = await shopService.UpdateShop(shopId, shopAddress, shopPhoneNumber, cancellationToken);
        return mapper.Map<ShopApi>(shop);
    }

    public async Task<bool> DeleteShop([Service] IShopService shopService,
        [Service] IMapper mapper,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        await shopService.DeleteShop(shopId, cancellationToken);
        return true;
    }

    public async Task<ShopCheckApi> CreateShopCheck([Service] IShopSaleService shopSaleService,
        [Service] IMapper mapper,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        var check = await shopSaleService.CreateShopCheck(shopId, cancellationToken);
        return mapper.Map<ShopCheckApi>(check);
    }

    public async Task<bool> AddProductToCheck([Service] IShopSaleService shopSaleService,
        Guid shopId,
        Guid productId,
        Guid checkId,
        int quantity,
        CancellationToken cancellationToken)
    {
        await shopSaleService.AddProductsToCheck(shopId, productId, checkId, quantity, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteShopCheck([Service] IShopSaleService shopSaleService,
        Guid checkId,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        await shopSaleService.DeleteShopCheck(checkId, shopId, cancellationToken);
        return true;
    }
}