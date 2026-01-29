using BUser.Domain.Interfaces;
using BUser.Domain.Model;

namespace BUser.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User> GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByLogin(string login)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateUser(User user)
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
}