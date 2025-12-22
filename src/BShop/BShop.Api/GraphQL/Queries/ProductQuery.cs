using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Domain.Interfaces.Service;
using BShop.Models;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[ExtendObjectType(Name = "Query")]
public class ProductQuery(IProductService _productService, IMapper _mapper)
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductApi> GetAllProducts()
    {
        return _productService.GetAllProducts().ProjectTo<ProductApi>(_mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductApi> GetProductById(Guid id)
    {
        return GetAllProducts()
            .Where(x => x.Id == id)
            .ProjectTo<ProductApi>(_mapper.ConfigurationProvider);
    }
}