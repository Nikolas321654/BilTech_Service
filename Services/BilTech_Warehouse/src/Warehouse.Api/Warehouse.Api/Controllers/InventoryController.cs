using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Request;
using Warehouse.Domain.Interfaces.Services;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("warehouse-api/[controller]")]
public class InventoryController(IWarehouseInventoryService inventoryService) : ControllerBase
{
    [HttpGet("{warehouseId:guid}")]
    public IActionResult GetWarehouseInventory(Guid warehouseId, CancellationToken cancellationToken)
    {
        var inventory = inventoryService.GetAllProductsFromWarehouse(warehouseId, cancellationToken);
        return Ok(inventory);
    }

    [HttpGet("{warehouseId:guid}/{productId:guid}")]
    public async Task<IActionResult> GetProductFromInventory(Guid warehouseId, Guid productId,
        CancellationToken cancellationToken)
    {
        var product = await inventoryService.GetProductFromWarehouse(warehouseId, productId, cancellationToken);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToInventory([FromBody] WarehouseInventory request,
        CancellationToken cancellationToken)
    {
        await inventoryService.AddProductToWarehouse(request.WarehouseId, request.ProductId, request.Quantity,
            request.ProductPrice, cancellationToken);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductInInventory([FromBody] WarehouseInventory request,
        CancellationToken cancellationToken)
    {
        await inventoryService.UpdateProductInWarehouseInventory(request.WarehouseId, request.ProductId,
            request.Quantity, request.ProductPrice, cancellationToken);
        return Ok();
    }

    [HttpDelete("{warehouseId:guid}/{productId:guid}")]
    public async Task<IActionResult> DeleteProductFromInventory(Guid warehouseId, Guid productId,
        CancellationToken cancellationToken)
    {
        await inventoryService.DeleteProductFromWarehouse(warehouseId, productId, cancellationToken);
        return Ok();
    }
}