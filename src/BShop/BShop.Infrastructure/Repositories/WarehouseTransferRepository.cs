using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class WarehouseTransferRepository(ShopDbContext context) : IWarehouseTransferRepository
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId)
    {
        return context.WarehouseTransferRequests
            .Where(x => x.ShopId == shopId && x.IsDeleted == false)
            .AsNoTracking();
    }

    public async Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId)
    {
        return await context.WarehouseTransferRequests
            .FirstOrDefaultAsync(x => x.Id == orderId && x.ShopId == shopId);
    }

    public async Task CreateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest)
    {
        await context.WarehouseTransferRequests.AddAsync(warehouseTransferRequest);
    }

    public void UpdateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest)
    {
        context.WarehouseTransferRequests.Update(warehouseTransferRequest);
    }

    public async Task DeleteWarehouseOrder(Guid shopId, Guid orderId)
    {
        var order = await GetWarehouseOrderById(shopId, orderId);
        if (order != null) context.Remove(order);
    }

    public async Task AddProductToOrder(WarehouseOrder order)
    {
        await context.WarehouseOrders.AddAsync(order);
    }

    public async Task<WarehouseOrder?> GetWarehouseOrderProduct(Guid orderId, Guid productId)
    {
        var order = await context.WarehouseOrders.FirstOrDefaultAsync(x =>
            x.OrderId == orderId && x.ProductId == productId);

        return order;
    }

    public async Task UpdateWarehouseOrderProduct(Guid productId, Guid orderId, int quantity)
    {
        await context.WarehouseOrders
            .Where(x => x.OrderId == orderId && x.ProductId == productId)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(o => o.ProductCount, o => o.ProductCount + quantity));
    }
}