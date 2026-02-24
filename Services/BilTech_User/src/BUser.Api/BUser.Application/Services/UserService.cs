using BUser.Domain.Interfaces;
using BUser.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Roles = BUser.Domain.Roles;

namespace BShop.Application.Services;

public class UserService(IUserRepository userRepository, IJwtProvider jwtProvider, IUserFabric userFabric)
    : IUserService
{
    private readonly PasswordHasher<UserEntity> _passwordHasher = new();

    public async Task<UserEntity?> GetUserById(Guid id, CancellationToken ct)
    {
        if (Guid.Empty == id) throw new ArgumentNullException($"Id cannot be empty, {id}");

        return await userRepository.GetUserById(id, ct) ?? throw new ArgumentNullException($"User not found, {id}");
    }

    public async Task<UserEntity> RegisterUser(string name, string login, string password, Roles role, Guid workPlaceId,
        string phoneNumber, CancellationToken ct)
    {
        var user = userFabric.CreateUser(name, login, password, role, workPlaceId, phoneNumber);

        var hashedPassword = _passwordHasher.HashPassword(user, password);
        user.Password = hashedPassword;

        return await userRepository.CreateUser(user, ct);
    }

    public async Task<UserEntity> RegisterOwner(string name, string login, string password, Roles role,
        string phoneNumber, CancellationToken ct)
    {
        var user = userFabric.CreateOwner(name, login, password, role, phoneNumber);

        var hashedPassword = _passwordHasher.HashPassword(user, password);
        user.Password = hashedPassword;

        return await userRepository.CreateUser(user, ct);
    }


    public async Task UpdateUser(Guid userId, string login, string password, string phoneNumber, CancellationToken ct)
    {
        if (Guid.Empty == userId) throw new ArgumentNullException($"Id cannot be empty, {userId}");
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            throw new ArgumentNullException(
                $"Error: PhoneNumber {phoneNumber}, Login {login} or Password cannot be null or empty");

        var user = await userRepository.GetUserById(userId, ct);
        if (user == null) throw new ArgumentNullException($"User not found, {userId}");

        var hashedPassword = _passwordHasher.HashPassword(user, password);

        user!.Login = login;
        user.Password = hashedPassword;
        user.PhoneNumber = phoneNumber;
        await userRepository.UpdateUser(user, ct);
    }

    public async Task DeleteUser(Guid id, CancellationToken ct)
    {
        if (Guid.Empty == id) throw new ArgumentNullException($"Id cannot be empty, {id}");

        await userRepository.DeleteUser(id, ct);
    }

    public IQueryable<UserEntity> GetAllUsers(Guid workPlaceId, CancellationToken ct)
    {
        return Guid.Empty == workPlaceId
            ? throw new ArgumentNullException($"WorkPlaceId cannot be empty, {workPlaceId}")
            : userRepository.GetAllUsers(workPlaceId, ct);
    }

    public async Task<string> Login(string login, string password, CancellationToken ct)
    {
        var user = await userRepository.GetUserByLogin(login, ct);
        if (user == null)
            throw new ArgumentNullException($"User not found, {login}");

        var result = new PasswordHasher<UserEntity>().VerifyHashedPassword(user, user.Password, password);
        if (result != PasswordVerificationResult.Success)
            throw new InvalidOperationException("Invalid login or password");

        var token = jwtProvider.GenerateJwtToken(user);
        return token;
    }
}