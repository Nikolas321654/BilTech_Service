using BShop.Domain.CustomExepions;
using BShop.Domain.Interfaces;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;
using BShop.Infrastructure;

namespace BShop.Application.Services;

public class WarehouseTransferService(IWarehouseTransferRepository warehouseRepository, IUnitOfWork unitOfWork)
    : IWarehouseTransferService
{
    public IQueryable<WarehouseTransferRequest> GetAllWarehouseOrders(Guid shopId)
    {
        return shopId == Guid.Empty
            ? throw new BadRequestException("Shop Id cannot be empty")
            : warehouseRepository.GetAllWarehouseOrders(shopId);
    }

    public async Task<WarehouseTransferRequest?> GetWarehouseOrderById(Guid shopId, Guid orderId)
    {
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");

        var order = await warehouseRepository.GetWarehouseOrderById(shopId, orderId);
        return order ?? throw new NotFoundException($"Warehouse order with {orderId} id, not found");
    }

    public async Task CreateWarehouseOrder(Guid shopId, Guid warehouseId)
    {
        if (warehouseId == Guid.Empty) throw new BadRequestException("Warehouse Id cannot be empty");
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");

        var newOrder = new WarehouseTransferRequest()
        {
            Id = Guid.NewGuid(),
            ShopId = shopId,
            WarehouseId = warehouseId,
            Status = nameof(OrderStatusEnums.Pending),
            DeliveredAt = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await warehouseRepository.CreateWarehouseOrder(newOrder);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateWarehouseOrder(Guid shopId, Guid orderId, OrderStatusEnums status, DateTime? deliveryDate)
    {
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");

        var order = await GetWarehouseOrderById(shopId, orderId);

        order!.Status = status.ToString();
        order.DeliveredAt = deliveryDate;
        order.UpdatedAt = DateTime.UtcNow;

        warehouseRepository.UpdateWarehouseOrder(order);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteWarehouseOrder(Guid shopId, Guid orderId)
    {
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");

        await warehouseRepository.GetWarehouseOrderById(shopId, orderId);
        await GetWarehouseOrderById(shopId, orderId);
        await warehouseRepository.DeleteWarehouseOrder(shopId, orderId);
    }

    public async Task AddProductToWarehouseOrder(Guid shopId, Guid orderId, Guid productId, int quantity)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            if (quantity <= 0) throw new BadRequestException("Quantity must be greater than 0");
            if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");
            if (productId == Guid.Empty) throw new BadRequestException("Product Id cannot be empty");

            var orderCheck = await warehouseRepository.GetWarehouseOrderById(shopId, orderId);
            if (orderCheck == null) throw new NotFoundException($"Warehouse order with {orderId} id, not found");

            var order = await warehouseRepository.GetWarehouseOrderProduct(orderId, productId);

            if (order == null)
            {
                var newOrder = new WarehouseOrder()
                {
                    OrderId = orderId,
                    ProductId = productId,
                    ProductCount = quantity
                };

                orderCheck.UpdatedAt = DateTime.UtcNow;
                await warehouseRepository.AddProductToOrder(newOrder);
            }
            else
            {
                await warehouseRepository.UpdateWarehouseOrderProduct(productId, orderId, quantity);
            }

            warehouseRepository.UpdateWarehouseOrder(orderCheck);
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