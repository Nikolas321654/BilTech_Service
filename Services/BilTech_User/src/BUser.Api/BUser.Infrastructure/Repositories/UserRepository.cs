using BUser.Domain.Interfaces;
using BUser.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BUser.Infrastructure.Repositories;

public class UserRepository(DbUserContext context) : IUserRepository
{
    public async Task<UserEntity?> GetUserById(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserEntity?> GetUserByLogin(string login)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Login == login);
    }

    public Task CreateUser(UserEntity userEntity)
    {
        context.Users.Add(userEntity);
        return context.SaveChangesAsync();
    }

    public Task UpdateUser(UserEntity userEntity)
    {
        context.Users.Update(userEntity);
        return context.SaveChangesAsync();
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }

    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId)
    {
        return context.Users
            .AsNoTracking()
            .Where(x => x.WorkPlaceId == workPlaceId);
    }
}