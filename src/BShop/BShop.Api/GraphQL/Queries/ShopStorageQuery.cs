using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Domain.Interfaces.Service;
using BShop.Models;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[ExtendObjectType(Name = "Query")]
public class ShopStorageQuery
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
}