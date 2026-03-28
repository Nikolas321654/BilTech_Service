using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Application.Models;
using BShop.Domain.Interfaces.Service;
using HotChocolate.Authorization;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[Authorize(Roles = ["ShopWorker", "Owner"])]
[ExtendObjectType(Name = "Query")]
public class ProductQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductApi> GetAllProducts([Service] IProductService productService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken)
    {
        return productService
            .GetAllProducts(cancellationToken)
            .ProjectTo<ProductApi>(mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductApi> GetProductById([Service] IProductService productService,
        [Service] IMapper mapper,
        Guid id,
        CancellationToken cancellationToken)
    {
        return productService.GetAllProducts(cancellationToken)
            .Where(p => p.Id == id)
            .ProjectTo<ProductApi>(mapper.ConfigurationProvider);
    }
}