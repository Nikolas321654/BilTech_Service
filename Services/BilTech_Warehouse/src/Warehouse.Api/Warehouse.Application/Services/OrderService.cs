using Warehouse.Domain.CustomExceptions;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Model;

namespace Warehouse.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IWarehouseInventoryService inventoryService,
    IWarehouseCheckService checkService,
    IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Order> CreateOrder(Guid warehouseId, List<(Guid ProductId, int Quantity)> items, CancellationToken cancellationToken)
    {
        if (items == null || items.Count == 0)
            throw new BadRequestException("Order must have at least one item.");

        await unitOfWork.BeginTransactionAsync();
        try
        {
            
            var order = new Order
            {
                Id = Guid.NewGuid(),
                WarehouseId = warehouseId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Packing,
                Items = items.Select(i => new OrderItem
                {
                    OrderId = Guid.Empty,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            foreach (var item in items)
            {
                await inventoryService.DecreaseProductQuantity(warehouseId, item.ProductId, item.Quantity, cancellationToken);
            }

            await orderRepository.CreateOrder(order, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            var check = await checkService.CreateWarehouseCheck(warehouseId, cancellationToken);
            foreach (var item in items)
            {
                await checkService.AddItemToWarehouseCheck(check.Id, warehouseId, item.ProductId, item.Quantity, cancellationToken);
            }

            await unitOfWork.CommitAsync();
            return order;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task MarkAsLeftWarehouse(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderById(orderId, cancellationToken);
        if (order == null)
            throw new NotFoundException("Order not found.");

        if (order.Status != OrderStatus.Packing)
            throw new BadRequestException($"Cannot change status from {order.Status} to LeftWarehouse.");

        order.Status = OrderStatus.LeftWarehouse;
        await orderRepository.UpdateOrder(order, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Order?> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        return await orderRepository.GetOrderById(orderId, cancellationToken);
    }

    public IQueryable<Order> GetWarehouseOrders(Guid warehouseId, CancellationToken cancellationToken)
    {
        return orderRepository.GetWarehouseOrders(warehouseId, cancellationToken);
    }
}
