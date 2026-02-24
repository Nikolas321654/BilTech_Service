namespace BUser.Api.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public Guid? WorkPlaceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}