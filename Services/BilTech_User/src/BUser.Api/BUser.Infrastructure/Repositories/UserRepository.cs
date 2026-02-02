using BUser.Domain.Interfaces;
using BUser.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BUser.Infrastructure.Repositories;

public class UserRepository(DbUserContext context) : IUserRepository
{
    public async Task<UserEntity?> GetUserById(Guid id, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);
    }

    public async Task<UserEntity?> GetUserByLogin(string login, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Login == login, cancellationToken: ct);
    }

    public async Task<UserEntity> CreateUser(UserEntity userEntity, CancellationToken ct)
    {
        context.Users.Add(userEntity);
        await context.SaveChangesAsync(ct);
        return userEntity;
    }

    public Task UpdateUser(UserEntity userEntity, CancellationToken ct)
    {
        context.Users.Update(userEntity);
        return context.SaveChangesAsync(ct);
    }

    public async Task DeleteUser(Guid id, CancellationToken ct)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync(ct);
        }
    }

    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId, CancellationToken ct)
    {
        return context.Users
            .AsNoTracking()
            .Where(x => x.WorkPlaceId == workPlaceId);
    }
}