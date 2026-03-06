using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Request;
using Warehouse.Domain.Interfaces.Services;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("warehouse-api/[controller]")]
public class CheckController(IWarehouseCheckService checkService) : ControllerBase
{
    [HttpPost("create/{warehouseId:guid}")]
    public async Task<IActionResult> CreateCheck(Guid warehouseId, CancellationToken cancellationToken)
    {
        var check = await checkService.CreateWarehouseCheck(warehouseId, cancellationToken);
        return Ok(check);
    }

    [HttpPost("item")]
    public async Task<IActionResult> AddItemToCheck([FromBody] CheckItem request, CancellationToken cancellationToken)
    {
        await checkService.AddItemToWarehouseCheck(request.CheckId, request.WarehouseId, request.ProductId,
            request.Quantity, cancellationToken);
        return Ok();
    }

    [HttpDelete("item/{checkId:guid}/{warehouseId:guid}/{productId:guid}")]
    public async Task<IActionResult> RemoveItemFromCheck(Guid checkId, Guid warehouseId, Guid productId,
        CancellationToken cancellationToken)
    {
        await checkService.RemoveItemFromWarehouseCheck(checkId, warehouseId, productId, cancellationToken);
        return Ok();
    }

    [HttpGet("{checkId:guid}/{warehouseId:guid}")]
    public async Task<IActionResult> GetCheck(Guid checkId, Guid warehouseId, CancellationToken cancellationToken)
    {
        var check = await checkService.GetWarehouseCheckById(checkId, warehouseId, cancellationToken);
        return Ok(check);
    }

    [HttpGet("warehouse/{warehouseId:guid}")]
    public IActionResult GetWarehouseChecks(Guid warehouseId, CancellationToken cancellationToken)
    {
        var checks = checkService.GetAllWarehouseChecks(warehouseId, cancellationToken);
        return Ok(checks);
    }

    [HttpDelete("{checkId:guid}/{warehouseId:guid}")]
    public async Task<IActionResult> DeleteCheck(Guid checkId, Guid warehouseId, CancellationToken cancellationToken)
    {
        await checkService.DeleteWarehouseCheck(checkId, warehouseId, cancellationToken);
        return Ok();
    }
}