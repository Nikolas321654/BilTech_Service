namespace Warehouse.Api.Models.Request;

public class ProductType
{
    Guid Id { get; set; }
    string Type { get; set; } = string.Empty;
    bool IsDeleted { get; set; } = false;
}