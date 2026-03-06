using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure.Repositories;

public class OrderRepository(WarehouseDbContext dbContext) : IOrderRepository
{
    public async Task<Order?> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId && !o.IsDeleted, cancellationToken);
    }

    public async Task CreateOrder(Order order, CancellationToken cancellationToken)
    {
        await dbContext.Orders.AddAsync(order, cancellationToken);
    }

    public async Task UpdateOrder(Order order, CancellationToken cancellationToken)
    {
        dbContext.Orders.Update(order);
    }

    public IQueryable<Order> GetWarehouseOrders(Guid warehouseId, CancellationToken cancellationToken)
    {
        return dbContext.Orders
            .Include(o => o.Items)
            .Where(o => o.WarehouseId == warehouseId && !o.IsDeleted);
    }
}
