using System.ComponentModel.DataAnnotations;

namespace Warehouse.Domain.Model;

public class Warehouse
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}