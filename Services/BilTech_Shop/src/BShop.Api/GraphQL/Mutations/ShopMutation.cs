using System.Data.Common;
using System.Security.Claims;
using AutoMapper;
using BShop.Application.Models;
using BShop.Domain.Interfaces.Service;
using HotChocolate.Authorization;

namespace BShop.GraphQL.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class ShopMutation
{
    [Authorize(Roles = ["Owner"])]
    public async Task<ShopApi> RegisterShop([Service] IShopService shopService,
        [Service] IMapper mapper,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        string shopName,
        string shopAddress,
        string shopPhoneNumber,
        CancellationToken cancellationToken)
    {
        var shop = await shopService.CreateShop(claimsPrincipal.GetUserId(),
            shopName,
            shopAddress,
            shopPhoneNumber,
            cancellationToken);

        return mapper.Map<ShopApi>(shop);
    }

    [Authorize(Roles = ["Owner"])]
    public async Task<ShopApi> UpdateShop([Service] IShopService shopService,
        [Service] IMapper mapper,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        Guid shopId,
        string shopName,
        string shopAddress,
        string shopPhoneNumber,
        CancellationToken cancellationToken)
    {
        var shop = await shopService.UpdateShop(shopId, claimsPrincipal.GetUserId(), shopName, shopAddress,
            shopPhoneNumber,
            cancellationToken);
        return mapper.Map<ShopApi>(shop);
    }

    [Authorize(Roles = ["Owner"])]
    public async Task<bool> DeleteShop([Service] IShopService shopService,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        await shopService.DeleteShop(shopId, claimsPrincipal.GetUserId(), cancellationToken);
        return true;
    }

    [Authorize(Roles = ["ShopWorker", "Owner"])]
    public async Task<ShopCheckApi> CreateShopCheck([Service] IShopSaleService shopSaleService,
        [Service] IMapper mapper,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var check = await shopSaleService.CreateShopCheck(claimsPrincipal.GetWorkPlaceId(), cancellationToken);
        return mapper.Map<ShopCheckApi>(check);
    }

    [Authorize(Roles = ["ShopWorker", "Owner"])]
    public async Task<bool> AddProductToCheck([Service] IShopSaleService shopSaleService,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        Guid productId,
        Guid checkId,
        int quantity,
        CancellationToken cancellationToken)
    {
        await shopSaleService.AddProductsToCheck(claimsPrincipal.GetWorkPlaceId(),
            productId,
            checkId,
            quantity,
            cancellationToken);

        return true;
    }

    [Authorize(Roles = ["ShopWorker", "Owner"])]
    public async Task<bool> DeleteShopCheck([Service] IShopSaleService shopSaleService,
        Guid checkId,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        await shopSaleService.DeleteShopCheck(checkId, claimsPrincipal.GetWorkPlaceId(), cancellationToken);
        return true;
    }
}