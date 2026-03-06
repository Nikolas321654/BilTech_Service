namespace Warehouse.Api.Models.Response;

public class ProductType
{
    Guid Id { get; set; }
    string Type { get; set; } = string.Empty;
    bool IsDeleted { get; set; } = false;
}