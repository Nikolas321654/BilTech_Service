using AutoMapper;
using AutoMapper.QueryableExtensions;
using BShop.Application.Models;
using BShop.Domain.Interfaces.Service;
using HotChocolate.Authorization;
using HotChocolate.Data;

namespace BShop.GraphQL.Queries;

[Authorize(Roles = ["ShopWorker", "Owner"])]
[ExtendObjectType(Name = "Query")]
public class WarehouseTransferQuery(IMapper mapper, IWarehouseTransferService warehouseTransferService)
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<WarehouseTransferRequestApi> GetAllOrdersToWarehouse(Guid shopId,
        CancellationToken cancellationToken)
    {
        return warehouseTransferService
            .GetAllWarehouseOrders(shopId, cancellationToken)
            .ProjectTo<WarehouseTransferRequestApi>(mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<WarehouseTransferRequestApi> GetOrderToWarehouse(Guid shopId, Guid orderId,
        CancellationToken cancellationToken)
    {
        return warehouseTransferService
            .GetAllWarehouseOrders(shopId, cancellationToken)
            .Where(x => x.Id == orderId)
            .ProjectTo<WarehouseTransferRequestApi>(mapper.ConfigurationProvider);
    }
}