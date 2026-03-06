using Warehouse.Domain.CustomExceptions;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Model;

namespace Warehouse.Application.Services;

public class WarehouseCheckService(
    IWarehouseCheckRepository warehouseCheckRepository,
    IWarehouseInventoryService warehouseInventoryService,
    IUnitOfWork unitOfWork) : IWarehouseCheckService
{
    public async Task<WarehouseCheck> CreateWarehouseCheck(Guid warehouseId, CancellationToken cancellationToken)
    {
        if (warehouseId == Guid.Empty) throw new BadRequestException("WarehouseId is required.");

        var check = new WarehouseCheck
        {
            Id = Guid.NewGuid(),
            WarehouseId = warehouseId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false,
            TotalPrice = 0
        };

        await warehouseCheckRepository.CreateWarehouseCheck(check, cancellationToken);
        return check;
    }

    public async Task AddItemToWarehouseCheck(Guid checkId, Guid warehouseId, Guid productId, int quantity,
        CancellationToken cancellationToken)
    {
        var check = await warehouseCheckRepository.GetWarehouseCheckById(checkId, warehouseId, cancellationToken);
        if (check == null) throw new NotFoundException("Check not found.");

        var inventory =
            await warehouseInventoryService.GetProductFromWarehouse(warehouseId, productId, cancellationToken);

        if (inventory == null) throw new NotFoundException("Product not found in warehouse.");
        if (inventory.Quantity < quantity) throw new BadRequestException("Not enough product in warehouse.");

        var existingItem = await warehouseCheckRepository.GetItemFromCheck(checkId, productId, cancellationToken);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            existingItem.TotalPrice = existingItem.Quantity * inventory.ProductPrice;
            await warehouseCheckRepository.UpdateCheckItem(existingItem, cancellationToken);
        }
        else
        {
            var newItem = new CheckItem
            {
                Id = Guid.NewGuid(),
                OrderId = checkId,
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = quantity * inventory.ProductPrice
            };
            await warehouseCheckRepository.AddItemToCheck(newItem, cancellationToken);
        }

        check.TotalPrice += quantity * inventory.ProductPrice;
        check.UpdatedAt = DateTime.UtcNow;
        await warehouseCheckRepository.UpdateWarehouseCheck(check, cancellationToken);

        await warehouseInventoryService.DecreaseProductQuantity(warehouseId, productId, quantity, cancellationToken);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveItemFromWarehouseCheck(Guid checkId, Guid warehouseId, Guid productId,
        CancellationToken cancellationToken)
    {
        var check = await warehouseCheckRepository.GetWarehouseCheckById(checkId, warehouseId, cancellationToken);
        if (check == null) throw new NotFoundException("Check not found.");

        var item = await warehouseCheckRepository.GetItemFromCheck(checkId, productId, cancellationToken);
        if (item == null) throw new NotFoundException("Item not found in check.");

        check.TotalPrice -= item.TotalPrice;
        check.UpdatedAt = DateTime.UtcNow;

        var inventory =
            await warehouseInventoryService.GetProductFromWarehouse(warehouseId, productId, cancellationToken);
        if (inventory != null)
        {
            await warehouseInventoryService.UpdateProductInWarehouseInventory(
                warehouseId, productId, inventory.Quantity + item.Quantity, inventory.ProductPrice, cancellationToken);
        }

        await warehouseCheckRepository.RemoveItemFromCheck(checkId, productId, cancellationToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteWarehouseCheck(Guid checkId, Guid warehouseId, CancellationToken cancellationToken)
    {
        await warehouseCheckRepository.DeleteWarehouseCheck(checkId, warehouseId, cancellationToken);
    }

    public async Task<WarehouseCheck?> GetWarehouseCheckById(Guid checkId, Guid warehouseId,
        CancellationToken cancellationToken)
    {
        return await warehouseCheckRepository.GetWarehouseCheckById(checkId, warehouseId, cancellationToken);
    }

    public IQueryable<WarehouseCheck> GetAllWarehouseChecks(Guid warehouseId, CancellationToken cancellationToken)
    {
        return warehouseCheckRepository.GetAllWarehouseChecks(warehouseId, cancellationToken);
    }
}