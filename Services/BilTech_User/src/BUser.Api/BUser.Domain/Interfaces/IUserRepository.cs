using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserRepository
{
    public Task<UserEntity?> GetUserById(Guid id);
    public Task<UserEntity?> GetUserByLogin(string login);
    public Task CreateUser(UserEntity userEntity);
    public Task UpdateUser(UserEntity userEntity);
    public Task DeleteUser(Guid id);
    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId);
}