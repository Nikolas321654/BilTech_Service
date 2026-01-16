using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class WarehouseTransferRepository(ShopDbContext context)
    : IWarehouseTransferRepository
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId, CancellationToken cancellationToken)
    {
        return context.WarehouseTransferRequests
            .Where(x => x.ShopId == shopId && x.IsDeleted == false)
            .AsNoTracking();
    }

    public async Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId,
        CancellationToken cancellationToken)
    {
        return await context.WarehouseTransferRequests
            .FirstOrDefaultAsync(x => x.Id == orderId && x.ShopId == shopId, cancellationToken);
    }

    public async Task CreateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest,
        CancellationToken cancellationToken)
    {
        await context.WarehouseTransferRequests.AddAsync(warehouseTransferRequest, cancellationToken);
    }

    public void UpdateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest,
        CancellationToken cancellationToken)
    {
        context.WarehouseTransferRequests.Update(warehouseTransferRequest);
    }

    public async Task DeleteWarehouseOrder(Guid shopId, Guid orderId, CancellationToken cancellationToken)
    {
        var order = await GetWarehouseOrderById(shopId, orderId, cancellationToken);
        if (order != null) context.Remove(order);
    }

    public async Task AddProductToOrder(WarehouseOrder order, CancellationToken cancellationToken)
    {
        await context.WarehouseOrders.AddAsync(order, cancellationToken);
    }

    public async Task<WarehouseOrder?> GetWarehouseOrderProduct(Guid orderId, Guid productId,
        CancellationToken cancellationToken)
    {
        var order = await context.WarehouseOrders.FirstOrDefaultAsync(x =>
            x.OrderId == orderId && x.ProductId == productId, cancellationToken);

        return order;
    }

    public async Task UpdateWarehouseOrderProduct(Guid productId, Guid orderId, int quantity,
        CancellationToken cancellationToken)
    {
        await context.WarehouseOrders
            .Where(x => x.OrderId == orderId && x.ProductId == productId)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(o => o.ProductCount, o => o.ProductCount + quantity), cancellationToken);
    }
}