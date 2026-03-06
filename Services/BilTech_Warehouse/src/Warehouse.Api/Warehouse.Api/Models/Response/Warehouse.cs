namespace Warehouse.Api.Models.Response;

public class Warehouse
{
    Guid Id { get; set; }
    Guid OwnerId { get; set; }
    string Address { get; set; } = string.Empty;
    string PhoneNumber { get; set; } = string.Empty;
    bool IsDeleted { get; set; } = false;
    DateTime CreatedAt { get; set; }
}