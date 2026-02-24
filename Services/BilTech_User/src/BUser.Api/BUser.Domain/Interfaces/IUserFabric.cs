using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserFabric
{
    public UserEntity CreateUser(string name, string login, string password, Roles role, Guid workPlaceId,
        string phoneNumber);

    public UserEntity CreateOwner(string name, string login, string password, Roles role, string phoneNumber);
}