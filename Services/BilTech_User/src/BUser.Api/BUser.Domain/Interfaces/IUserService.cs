using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserService
{
    public Task<UserEntity?> GetUserById(Guid id, CancellationToken ct);

    public Task<UserEntity> RegisterUser(string name, string login, string password, Guid workPlaceId,
        string phoneNumber, CancellationToken ct);

    public Task UpdateUser(Guid userId, string login, string password, string phoneNumber, CancellationToken ct);
    public Task DeleteUser(Guid id, CancellationToken ct);
    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId, CancellationToken ct);

    public Task<UserEntity> RegisterOwner(string name, string login, string password, Roles role,
        string phoneNumber, CancellationToken ct);

    public Task<string> Login(string login, string password, CancellationToken ct);
}