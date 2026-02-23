using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using HotChocolate.Authorization;

namespace BShop.GraphQL.Mutations;

[Authorize(Roles = ["ShopWorker", "Owner"])]
[ExtendObjectType(Name = "Mutation")]
public class WarehouseTransferMutation
{
    public async Task<bool> CreateWarehouseTransferRequest([Service] IWarehouseTransferService warehouseTransferService,
        Guid shopId, Guid warehouseId, CancellationToken cancellationToken)
    {
        await warehouseTransferService.CreateWarehouseOrder(shopId, cancellationToken);
        return true;
    }

    public async Task<bool> AddProductToRequest([Service] IWarehouseTransferService warehouseTransferService,
        Guid shopId, Guid orderId, Guid productId, int quantity,
        CancellationToken cancellationToken)
    {
        await warehouseTransferService.AddProductToWarehouseOrder(shopId, orderId, productId, quantity,
            cancellationToken);
        return true;
    }

    public async Task<bool> DeleteWarehouseOrder([Service] IWarehouseTransferService warehouseTransferService,
        Guid shopId, Guid orderId, CancellationToken cancellationToken)
    {
        await warehouseTransferService.DeleteWarehouseOrder(shopId, orderId, cancellationToken);
        return true;
    }
}