using BUser.Domain;
using BUser.Domain.Interfaces;
using BUser.Domain.Model;

namespace BShop.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task<User> GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByLogin(string login)
    {
        throw new NotImplementedException();
    }

    public Task<User> RegisterUser(string name, string login, string password, Roles role, Guid workPlaceId)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<string> Login(string login, string password)
    {
        throw new NotImplementedException();
    }
}