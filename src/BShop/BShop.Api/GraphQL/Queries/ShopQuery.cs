using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Domain.Interfaces.Service;
using BShop.Models;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[ExtendObjectType(Name = "Query")]
public class ShopQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ShopStorageApi> GetAllShopProducts([Service] IMapper mapper,
        [Service] IShopStorageService shopStorageService,
        Guid shopId,
        CancellationToken cancellationToken)
    {
        return shopStorageService
            .GetAllProductsFromShop(shopId, cancellationToken)
            .ProjectTo<ShopStorageApi>(mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ShopStorageApi> GetShopProductById([Service] IMapper mapper,
        [Service] IShopStorageService shopStorageService,
        Guid shopId,
        Guid productId,
        CancellationToken cancellationToken)
    {
        return shopStorageService
            .GetAllProductsFromShop(shopId, cancellationToken)
            .Where(x => x.ProductId == productId)
            .ProjectTo<ShopStorageApi>(mapper.ConfigurationProvider);
    }

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