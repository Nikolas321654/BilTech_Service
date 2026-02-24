using BUser.Domain;
using BUser.Domain.Interfaces;
using BUser.Domain.Model;

namespace BShop.Application;

public class UserFabric : IUserFabric
{
    public UserEntity CreateOwner(string name, string login, string password, Roles role, string phoneNumber)
    {
        ValidateCommonFields(name, login, password, phoneNumber);
        return new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Login = login,
            Role = role,
            WorkPlaceId = null,
            PhoneNumber = phoneNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public UserEntity CreateUser(string name, string login, string password, Roles role, Guid workPlaceId,
        string phoneNumber)
    {
        if (workPlaceId == Guid.Empty) throw new ArgumentNullException(nameof(workPlaceId));
        ValidateCommonFields(name, login, password, phoneNumber);
        if (role == Roles.Owner) throw new InvalidOperationException("Cannot register a user with Owner role");
        
        return new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Login = login,
            Role = role,
            WorkPlaceId = workPlaceId,
            PhoneNumber = phoneNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    private static void ValidateCommonFields(string name, string login, string password, string phone)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException($"Name cannot be null or empty");
        if (string.IsNullOrEmpty(login)) throw new ArgumentNullException($"Login cannot be null or empty");
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters long.");
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone number is required.", nameof(phone));
    }
}
