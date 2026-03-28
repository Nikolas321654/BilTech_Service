using AutoMapper;
using BShop.Application.Builders;
using BShop.Application.Models;
using BShop.Domain.CustomExceptions;
using BShop.Domain.Interfaces;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.Domain.Model;
using Messaging.Kafka;

namespace BShop.Application.Services;

public class WarehouseTransferService(
    IWarehouseTransferRepository warehouseRepository,
    IUnitOfWork unitOfWork,
    IKafkaProducer<WarehouseTransferRequestApi> kafkaProducer,
    IMapper mapper)
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
        if (shopId == Guid.Empty) throw new BadRequestException("Shop Id cannot be empty");
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");

        var order = await warehouseRepository.GetWarehouseOrderById(shopId, orderId, cancellationToken);
        return order ?? throw new NotFoundException($"Warehouse order with {orderId} id, not found");
    }

    public async Task CreateWarehouseOrder(Guid shopId, CancellationToken cancellationToken)
    {
        var newOrder = new WarehouseRequestBuilder()
            .AddId()
            .AddCreatedAt()
            .AddDeliveredAt(null)
            .AddShopId(shopId)
            .AddAtIsDeleted()
            .FinishBuild();

        await warehouseRepository.CreateWarehouseOrder(newOrder, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task SendWarehouseOrder(Guid shopId, Guid orderId, CancellationToken cancellationToken)
    {
        var order = await GetWarehouseOrderById(shopId, orderId, cancellationToken);
        var message = mapper.Map<WarehouseTransferRequestApi>(order);
        await kafkaProducer.ProduceAsync(message.Id.ToString(), message, cancellationToken);
    }

    public async Task UpdateWarehouseOrder(Guid shopId, Guid orderId, DateTime? deliveryDate,
        CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty) throw new BadRequestException("Order Id cannot be empty");

        var order = await GetWarehouseOrderById(shopId, orderId, cancellationToken);
        if (order == null) throw new NotFoundException($"Warehouse order with {orderId} id, not found");

        order.DeliveredAt = deliveryDate;

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
            var orderCheck = await warehouseRepository.GetWarehouseOrderById(shopId, orderId, cancellationToken);
            if (orderCheck == null) throw new NotFoundException($"Warehouse order with {orderId} id, not found");

            var order = await warehouseRepository.GetWarehouseOrderProduct(orderId, productId, cancellationToken);

            if (order == null)
            {
                var newOrder = new WarehouseTransferItemBuilder()
                    .AddOrderId(orderId)
                    .AddProductId(productId)
                    .AddProductCount(quantity)
                    .FinishBuild();

                await warehouseRepository.AddProductToOrder(newOrder, cancellationToken);
            }
            else
            {
                await warehouseRepository.UpdateWarehouseOrderProduct(productId, orderId, quantity, cancellationToken);
            }

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