using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Interfaces.Services;

namespace Warehouse.Api.Controllers;

[Route("types")]
public class ProductTypeController(IProductTypeService productService) : Controller
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductTypeById(Guid id, CancellationToken cancellationToken)
    {
        var type = await productService.GetProductTypeById(id, cancellationToken);
        return Ok(type);
    }

    [HttpGet("")]
    public IActionResult GetAllProductTypes()
    {
        var types = productService.GetAllProductTypes();
        return Ok(types);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProductType([FromBody] string typeName, CancellationToken cancellationToken)
    {
        var type = await productService.CreateProductType(typeName, cancellationToken);
        return CreatedAtAction(nameof(GetProductTypeById), new { id = type.Id }, type);
    }
}