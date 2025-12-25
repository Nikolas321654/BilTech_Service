using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class WarehouseTransferRepository(ShopDbContext context) : IWarehouseTransferRepository
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId)
    {
        return context.WarehouseTransferRequests
            .Where(x => x.ShopId == shopId)
            .AsNoTracking();
    }

    public async Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId)
    {
        return await context.WarehouseTransferRequests
            .FirstOrDefaultAsync(x => x.Id == orderId && x.ShopId == shopId);
    }

    public async Task CreateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest)
    {
        context.WarehouseTransferRequests.Add(warehouseTransferRequest);
        await context.SaveChangesAsync();
    }

    public async Task UpdateWarehouseOrder(WarehouseTransferRequest warehouseTransferRequest)
    {
        context.WarehouseTransferRequests.Update(warehouseTransferRequest);
        await context.SaveChangesAsync();
    }

    public async Task DeleteWarehouseOrder(Guid shopId, Guid orderId)
    {
        var order = await GetWarehouseOrderById(shopId, orderId);
        if (order != null) context.Remove(order);
        await context.SaveChangesAsync();
    }

    public async Task EddProductToWarehouse(WarehouseOrder order)
    {
        await context.WarehouseOrders.AddAsync(order);
        await context.SaveChangesAsync();
    }
}