using BUser.Domain.Interfaces;
using BUser.Domain.Model;
using Roles = BUser.Domain.Roles;

namespace BShop.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserEntity?> GetUserById(Guid id)
    {
        if (Guid.Empty == id) throw new ArgumentNullException($"Id cannot be empty, {id}");

        return await userRepository.GetUserById(id) ?? throw new ArgumentNullException($"User not found, {id}");
    }

    public async Task RegisterUser(string name, string login, string password, Roles role, Guid workPlaceId,
        string phoneNumber)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            throw new ArgumentNullException($"Error: Name {name}, Login {login} or Password cannot be null or empty");

        var existingUser = await userRepository.GetUserByLogin(login);
        if (existingUser != null) throw new InvalidOperationException("Login is already taken");

        await userRepository.CreateUser(new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Login = login,
            Password = password,
            Role = role,
            WorkPlaceId = workPlaceId,
            PhoneNumber = phoneNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task UpdateUser(Guid userId, string login, string password, string phoneNumber)
    {
        if (Guid.Empty == userId) throw new ArgumentNullException($"Id cannot be empty, {userId}");
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            throw new ArgumentNullException(
                $"Error: PhoneNumber {phoneNumber}, Login {login} or Password cannot be null or empty");

        var user = await userRepository.GetUserById(userId);

        user!.Login = login;
        user.Password = password;
        user.PhoneNumber = phoneNumber;
        await userRepository.UpdateUser(user);
    }

    public async Task DeleteUser(Guid id)
    {
        if (Guid.Empty == id) throw new ArgumentNullException($"Id cannot be empty, {id}");

        await userRepository.DeleteUser(id);
    }

    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId)
    {
        return userRepository.GetAllUsers(workPlaceId);
    }

    public async Task<string> Login(string login, string password)
    {
        return await userRepository.GetUserByLogin(login) == null
            ? throw new ArgumentNullException($"User not found, {login}")
            : "";
    }
}