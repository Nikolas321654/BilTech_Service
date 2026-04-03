using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Api.Models.Request;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("warehouse-api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var product = await productService.GetProductById(id, cancellationToken);
        return Ok(product);
    }

    [HttpGet]
    public IActionResult GetAllProducts(CancellationToken cancellationToken)
    {
        var products = productService.GetAllProducts(cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Models.Request.Product request, CancellationToken cancellationToken)
    {
        var product = await productService.CreateProduct(request.Name, request.Price, request.ProductTypeId, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Models.Request.Product request, CancellationToken cancellationToken)
    {
        await productService.UpdateProduct(id, request.Name, request.Price, request.ProductTypeId, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        await productService.DeleteProduct(id, cancellationToken);
        return NoContent();
    }
}