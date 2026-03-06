namespace Warehouse.Api.Models.Request;

public class Product
{
    public Guid Id { get; set; }
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}