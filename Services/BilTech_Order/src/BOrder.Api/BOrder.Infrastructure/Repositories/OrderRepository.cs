using BOrder.Domain.Interfaces;
using BOrder.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BOrder.Infrastructure.Repositories;

public class OrderRepository(OrderDbContext context) : IOrderRepository
{
    public async Task CreateOrder(OrderEntity order, CancellationToken cancellationToken)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<OrderEntity?> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        return await context.Orders.FindAsync(id, cancellationToken);
    }

    public Task UpdateOrder(OrderEntity order)
    {
        context.Orders.Update(order);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}