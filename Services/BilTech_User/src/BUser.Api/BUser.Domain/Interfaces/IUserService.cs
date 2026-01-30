using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserService
{
    public Task<UserEntity?> GetUserById(Guid id);

    public Task RegisterUser(string name, string login, string password, Roles role, Guid workPlaceId,
        string phoneNumber);

    public Task UpdateUser(Guid iserId, string login, string password, string phoneNumber);
    public Task DeleteUser(Guid id);
    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId);
    public Task<string> Login(string login, string password);
}