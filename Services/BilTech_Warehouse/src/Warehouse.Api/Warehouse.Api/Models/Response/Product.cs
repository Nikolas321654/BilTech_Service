namespace Warehouse.Api.Models.Response;

public class Product
{
    Guid Id { get; set; }
    Guid ProductTypeId { get; set; }
    string Name { get; set; } = string.Empty;
    decimal Price { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    bool IsDeleted { get; set; } = false;
}