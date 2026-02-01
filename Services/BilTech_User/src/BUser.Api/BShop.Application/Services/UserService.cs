using BUser.Domain.Interfaces;
using BUser.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Roles = BUser.Domain.Roles;

namespace BShop.Application.Services;

public class UserService(IUserRepository userRepository, IJwtProvider jwtProvider) : IUserService
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

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Login = login,
            Role = role,
            WorkPlaceId = workPlaceId,
            PhoneNumber = phoneNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(user, password);
        user.Password = hashedPassword;

        await userRepository.CreateUser(user);
    }

    public async Task UpdateUser(Guid userId, string login, string password, string phoneNumber)
    {
        if (Guid.Empty == userId) throw new ArgumentNullException($"Id cannot be empty, {userId}");
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            throw new ArgumentNullException(
                $"Error: PhoneNumber {phoneNumber}, Login {login} or Password cannot be null or empty");

        var user = await userRepository.GetUserById(userId);
        if (user == null) throw new ArgumentNullException($"User not found, {userId}");

        var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(user, password);

        user!.Login = login;
        user.Password = hashedPassword;
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
        return Guid.Empty == workPlaceId
            ? throw new ArgumentNullException($"WorkPlaceId cannot be empty, {workPlaceId}")
            : userRepository.GetAllUsers(workPlaceId);
    }

    public async Task<string> Login(string login, string password)
    {
        var user = await userRepository.GetUserByLogin(login);
        if (user == null)
            throw new ArgumentNullException($"User not found, {login}");

        var result = new PasswordHasher<UserEntity>().VerifyHashedPassword(user, user.Password, password);
        if (result != PasswordVerificationResult.Success)
            throw new InvalidOperationException("Invalid login or password");

        var token = jwtProvider.GenerateJwtToken(user);
        return token;
    }
}