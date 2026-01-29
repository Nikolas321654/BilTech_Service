using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User> GetUserById(Guid id);
    public Task<User> GetUserByLogin(string login);
    public Task<User> CreateUser(User user);
    public Task<User> UpdateUser(User user);
    public Task DeleteUser(Guid id);
    public Task<List<User>> GetAllUsers();
}