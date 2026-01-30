using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserService
{
    public Task<UserEntity> GetUserById(Guid id);
    public Task<UserEntity> RegisterUser(string name, string login, string password, Roles role, Guid workPlaceId);
    public Task<UserEntity> UpdateUser(UserEntity userEntity);
    public Task DeleteUser(Guid id);
    public Task<List<UserEntity>> GetAllUsers(Guid workPlaceId);
    public Task<string> Login(string login, string password);
}