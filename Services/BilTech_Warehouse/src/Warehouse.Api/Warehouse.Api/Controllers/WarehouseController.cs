using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Api.Models.Request;

namespace Warehouse.Api.Controllers;

[ApiController]
[Authorize(Roles = "Owner, WarehouseWorker")]
[Route("warehouse-api/[controller]")]
public class WarehouseController(IWarehouseService warehouseService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetWarehouseById(Guid id, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseService.GetWarehouseById(id, cancellationToken);
        return Ok(warehouse);
    }

    [HttpGet]
    public IActionResult GetAllWarehouses(CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
        var warehouses = warehouseService.GetAllWarehouses(ownerId, cancellationToken);
        return Ok(warehouses);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWarehouse([FromBody] Models.Request.Warehouse request,
        CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

        var warehouse = await warehouseService.CreateWarehouse(ownerId, request.Address, request.PhoneNumber,
            cancellationToken);
        return CreatedAtAction(nameof(GetWarehouseById), new { id = warehouse.Id }, warehouse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateWarehouse(Guid id, [FromBody] Models.Request.Warehouse request,
        CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
        await warehouseService.UpdateWarehouse(ownerId,
            id,
            request.Address,
            request.PhoneNumber,
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteWarehouse(Guid id, CancellationToken cancellationToken)
    {
        var ownerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
        await warehouseService.DeleteWarehouse(ownerId, id, cancellationToken);
        return NoContent();
    }
}