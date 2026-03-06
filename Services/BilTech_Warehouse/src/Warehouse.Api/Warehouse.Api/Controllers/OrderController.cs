using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Request;
using Warehouse.Domain.Interfaces.Services;
using DomainOrder = Warehouse.Domain.Model.Order;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("warehouse-api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<DomainOrder>> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var items = request.Items.Select(i => (i.ProductId, i.Quantity)).ToList();
        var order = await orderService.CreateOrder(request.WarehouseId, items, cancellationToken);
        return Ok(order);
    }

    [HttpPatch("{id:guid}/left-warehouse")]
    public async Task<IActionResult> MarkAsLeftWarehouse(Guid id, CancellationToken cancellationToken)
    {
        await orderService.MarkAsLeftWarehouse(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DomainOrder>> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderById(id, cancellationToken);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("warehouse/{warehouseId:guid}")]
    public ActionResult<IEnumerable<DomainOrder>> GetWarehouseOrders(Guid warehouseId, CancellationToken cancellationToken)
    {
        var orders = orderService.GetWarehouseOrders(warehouseId, cancellationToken);
        return Ok(orders);
    }
}