using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserService
{
    public Task<User> GetUserById(Guid id);
    public Task<User> GetUserByLogin(string login);
    public Task<User> RegisterUser(string name, string login, string password, Roles role, Guid workPlaceId);
    public Task<User> UpdateUser(User user);
    public Task DeleteUser(Guid id);
    public Task<List<User>> GetAllUsers();
    public Task<string> Login(string login, string password);
}