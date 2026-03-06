using Warehouse.Domain.CustomExceptions;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Model;

namespace Warehouse.Application.Services;

public class WarehouseInventoryService(
    IWarehouseInventoryRepository warehouseInventoryRepository) : IWarehouseInventoryService
{
    public async Task<WarehouseInventory?> GetProductFromWarehouse(Guid warehouseId, Guid productId,
        CancellationToken cancellationToken)
    {
        return await warehouseInventoryRepository.GetProductFromWarehouse(warehouseId, productId, cancellationToken);
    }

    public async Task AddProductToWarehouse(Guid warehouseId, Guid productId, int quantity, decimal productPrice,
        CancellationToken cancellationToken)
    {
        var existingInventory =
            await warehouseInventoryRepository.GetProductFromWarehouse(warehouseId, productId, cancellationToken);

        if (existingInventory != null)
            throw new BadRequestException("Product already exists in warehouse storage. Try updating its quantity.");

        var inventory = new WarehouseInventory
        {
            WarehouseId = warehouseId,
            ProductId = productId,
            Quantity = quantity,
            ProductPrice = productPrice
        };

        await warehouseInventoryRepository.AddProductToWarehouse(inventory, cancellationToken);
    }

    public async Task UpdateProductInWarehouseInventory(Guid warehouseId, Guid productId, int quantity,
        decimal productPrice, CancellationToken cancellationToken)
    {
        var inventory =
            await warehouseInventoryRepository.GetProductFromWarehouse(warehouseId, productId, cancellationToken);

        if (inventory == null) throw new NotFoundException("Product not found in warehouse.");

        inventory.Quantity = quantity;
        inventory.ProductPrice = productPrice;

        await warehouseInventoryRepository.UpdateProductInWarehouseInventory(inventory, cancellationToken);
    }

    public async Task DeleteProductFromWarehouse(Guid warehouseId, Guid productId, CancellationToken cancellationToken)
    {
        var inventory =
            await warehouseInventoryRepository.GetProductFromWarehouse(warehouseId, productId, cancellationToken);

        if (inventory == null) throw new NotFoundException("Product not found in warehouse.");

        await warehouseInventoryRepository.DeleteProductFromWarehouse(warehouseId, productId, cancellationToken);
    }

    public IQueryable<WarehouseInventory> GetAllProductsFromWarehouse(Guid warehouseId,
        CancellationToken cancellationToken)
    {
        return warehouseInventoryRepository.GetWarehouseAllProducts(warehouseId, cancellationToken);
    }

    public async Task DecreaseProductQuantity(Guid warehouseId, Guid productId, int quantity,
        CancellationToken cancellationToken)
    {
        var inventory =
            await warehouseInventoryRepository.GetProductFromWarehouse(warehouseId, productId, cancellationToken);

        if (inventory == null) throw new NotFoundException("Product not found in warehouse.");
        if (inventory.Quantity < quantity)
            throw new BadRequestException($"Not enough product in warehouse. Available: {inventory.Quantity}");

        inventory.Quantity -= quantity;
        await warehouseInventoryRepository.UpdateProductInWarehouseInventory(inventory, cancellationToken);
    }
}