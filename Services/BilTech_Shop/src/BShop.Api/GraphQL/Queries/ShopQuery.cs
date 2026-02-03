using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Domain.Interfaces.Service;
using BShop.Models;
using HotChocolate.Authorization;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[Authorize(Roles = ["ShopWorker", "Owner"])]
[ExtendObjectType(Name = "Query")]
public class ShopQuery
{
    [Authorize(Roles = ["ShopWorker", "Owner"])]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ShopCheckApi> GetAllShopChecks([Service] IMapper mapper,
        [Service] IShopSaleService shopSaleService,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        return shopSaleService
            .GetAllShopChecks(shopId, cancellationToken)
            .ProjectTo<ShopCheckApi>(mapper.ConfigurationProvider);
    }

    [Authorize(Roles = ["ShopWorker", "Owner"])]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ShopCheckApi> GetShopCheck([Service] IMapper mapper,
        [Service] IShopSaleService shopSaleService,
        Guid shopId,
        Guid checkId,
        CancellationToken cancellationToken)
    {
        return shopSaleService
            .GetAllShopChecks(shopId, cancellationToken)
            .Where(x => x.Id == checkId)
            .ProjectTo<ShopCheckApi>(mapper.ConfigurationProvider);
    }

    [Authorize(Roles = ["Owner"])]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ShopApi> GetShopById([Service] IShopService shopService,
        [Service] IMapper mapper,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        return shopService.GetAllShops(cancellationToken)
            .Where(x => x.Id == shopId)
            .ProjectTo<ShopApi>(mapper.ConfigurationProvider);
    }

    [Authorize(Roles = ["Owner"])]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ShopApi> GetAllShops([Service] IShopService shopService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken)
    {
        return shopService.GetAllShops(cancellationToken).ProjectTo<ShopApi>(mapper.ConfigurationProvider);
    }
}