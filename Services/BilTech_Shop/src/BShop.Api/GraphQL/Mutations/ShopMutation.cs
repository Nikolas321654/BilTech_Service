using System.Data.Common;
using System.Security.Claims;
using AutoMapper;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Models;
using HotChocolate.Authorization;

namespace BShop.GraphQL.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class ShopMutation
{
    [Authorize(Roles = ["Owner"])]
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

    [Authorize(Roles = ["Owner"])]
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

    [Authorize(Roles = ["Owner"])]
    public async Task<bool> DeleteShop([Service] IShopService shopService,
        [Service] IMapper mapper,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        await shopService.DeleteShop(shopId, cancellationToken);
        return true;
    }

    [Authorize(Roles = ["ShopWorker", "Owner"])]
    public async Task<ShopCheckApi> CreateShopCheck([Service] IShopSaleService shopSaleService,
        [Service] IMapper mapper,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var workPlaceIdClaim = claimsPrincipal.FindFirst("workPlaceId")?.Value;
        if (!Guid.TryParse(workPlaceIdClaim, out var shopId)) throw new GraphQLException("Invalid shop id");

        var check = await shopSaleService.CreateShopCheck(shopId, cancellationToken);
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
        var workPlaceIdClaim = claimsPrincipal.FindFirst("workPlaceId")?.Value;
        if (!Guid.TryParse(workPlaceIdClaim, out var shopId)) throw new GraphQLException("Invalid shop id");
        await shopSaleService.AddProductsToCheck(shopId, productId, checkId, quantity, cancellationToken);
        return true;
    }

    [Authorize(Roles = ["ShopWorker", "Owner"])]
    public async Task<bool> DeleteShopCheck([Service] IShopSaleService shopSaleService,
        Guid checkId,
        [GlobalState("ClaimsPrincipal")] ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var workPlaceIdClaim = claimsPrincipal.FindFirst("workPlaceId")?.Value;
        if (!Guid.TryParse(workPlaceIdClaim, out var shopId)) throw new GraphQLException("Invalid shop id");
        await shopSaleService.DeleteShopCheck(checkId, shopId, cancellationToken);
        return true;
    }
}