using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Application.Models;
using BShop.Domain.Interfaces.Service;
using HotChocolate.Authorization;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[Authorize(Roles = ["ShopWorker", "Owner"])]
[ExtendObjectType(Name = "Query")]
public class ProductTypeQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductTypeApi> GetAllProductTypes([Service] IProductTypeService productTypeService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken)
    {
        return productTypeService.GetAllProductTypes(cancellationToken)
            .ProjectTo<ProductTypeApi>(mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductTypeApi> GetProductTypeById([Service] IProductTypeService productTypeService,
        [Service] IMapper mapper,
        Guid id,
        CancellationToken cancellationToken)
    {
        return productTypeService.GetAllProductTypes(cancellationToken)
            .Where(x => x.Id == id)
            .ProjectTo<ProductTypeApi>(mapper.ConfigurationProvider);
    }
}