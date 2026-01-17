using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;

namespace BShop.Application.Services;

public class WarehouseTransferService(IWarehouseTransferRepository warehouseRepository, IUnitOfWork unitOfWork)
    : IWarehouseTransferService
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId, CancellationToken cancellationToken)
    {
        return shopId == Guid.Empty
            ? throw new BadRequestException("Shop Id cannot be empty")
            : warehouseRepository.GetAllWarehouseOrders(shopId, cancellationToken);
    }

    public async Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId,
        CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");

        var order = await warehouseRepository.GetWarehouseOrderById(shopId, orderId, cancellationToken);
        return order ?? throw new NotFoundException($"Warehouse order with {orderId} id, not found");
    }

    public async Task CreateWarehouseOrder(Guid shopId, Guid warehouseId, CancellationToken cancellationToken)
    {
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");

        var newOrder = new WarehouseTransferRequest()
        {
            Id = Guid.NewGuid(),
            ShopId = shopId,
            WarehouseId = warehouseId,
            Status = nameof(OrderStatus.Pending),
            DeliveredAt = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await warehouseRepository.CreateWarehouseOrder(newOrder, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateWarehouseOrder(Guid shopId, Guid orderId, OrderStatus status, DateTime? deliveryDate,
        CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");

        var order = await GetWarehouseOrderById(shopId, orderId, cancellationToken);

        order!.Status = status.ToString();
        order.DeliveredAt = deliveryDate;
        order.UpdatedAt = DateTime.UtcNow;

        warehouseRepository.UpdateWarehouseOrder(order, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteWarehouseOrder(Guid shopId, Guid orderId, CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");

        await GetWarehouseOrderById(shopId, orderId, cancellationToken);
        await warehouseRepository.DeleteWarehouseOrder(shopId, orderId, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddProductToWarehouseOrder(Guid shopId, Guid orderId, Guid productId, int quantity,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            if (quantity <= 0) throw new BadRequestException("Quantity must be greater than 0");
            if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");
            if (productId == Guid.Empty) throw new BadRequestException("Product Id cannot be empty");

            var orderCheck = await warehouseRepository.GetWarehouseOrderById(shopId, orderId, cancellationToken);
            if (orderCheck == null) throw new NotFoundException($"Warehouse order with {orderId} id, not found");

            var order = await warehouseRepository.GetWarehouseOrderProduct(orderId, productId, cancellationToken);

            if (order == null)
            {
                var newOrder = new WarehouseOrder()
                {
                    OrderId = orderId,
                    ProductId = productId,
                    ProductCount = quantity
                };

                orderCheck.UpdatedAt = DateTime.UtcNow;
                await warehouseRepository.AddProductToOrder(newOrder, cancellationToken);
            }
            else
            {
                await warehouseRepository.UpdateWarehouseOrderProduct(productId, orderId, quantity, cancellationToken);
                orderCheck.UpdatedAt = DateTime.UtcNow;
            }

            await unitOfWork.SaveChangesAsync();

            warehouseRepository.UpdateWarehouseOrder(orderCheck, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
// Write - off of goods from warehouse will be done when warehouse service will be ready