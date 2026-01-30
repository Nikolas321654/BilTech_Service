using BUser.Domain.Model;

namespace BUser.Domain.Interfaces;

public interface IUserRepository
{
    public Task<UserEntity> GetUserById(Guid id);
    public Task<UserEntity> GetUserByLogin(string login);
    public Task<UserEntity> CreateUser(UserEntity userEntity);
    public Task<UserEntity> UpdateUser(UserEntity userEntity);
    public Task DeleteUser(Guid id);
    public Task<List<UserEntity>> GetAllUsers(Guid workPlaceId);
}