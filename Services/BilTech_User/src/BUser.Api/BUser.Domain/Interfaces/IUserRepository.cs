using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserRepository
{
    public Task<UserEntity?> GetUserById(Guid id, CancellationToken ct);
    public Task<UserEntity?> GetUserByLogin(string login, CancellationToken ct);
    public Task<UserEntity> CreateUser(UserEntity userEntity, CancellationToken ct);
    public Task UpdateUser(UserEntity userEntity, CancellationToken ct);
    public Task DeleteUser(Guid id, CancellationToken ct);
    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId, CancellationToken ct);
}