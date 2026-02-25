using BUser.Api.Models;
using AutoMapper;
using BUser.Domain.Interfaces;
using HotChocolate.Authorization;

namespace BUser.Api.GraphQl.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class UserMutation
{
    [Authorize(Roles = ["Owner"])]
    public async Task<UserResponse> AddUser([Service] IUserService userService,
        [Service] IMapper mapper,
        string name,
        string login,
        string password,
        string phoneNumber,
        Domain.Roles role,
        Guid workPlaceId,
        CancellationToken ct)
    {
        var user = await userService.RegisterUser(name, login, password, role, workPlaceId, phoneNumber, ct);
        return mapper.Map<UserResponse>(user);
    }
    
    [AllowAnonymous]
    public async Task<UserResponse> AddOwner([Service] IUserService userService,
        [Service] IMapper mapper,
        string name,
        string login,
        string password,
        string phoneNumber,
        CancellationToken ct)
    {
        var user = await userService.RegisterOwner(name, login, password, Domain.Roles.Owner, phoneNumber, ct);
        return mapper.Map<UserResponse>(user);
    }

    [AllowAnonymous]
    public async Task<string> Login([Service] IUserService userService,
        string login,
        string password,
        CancellationToken ct)
    {
        return await userService.Login(login, password, ct);
    }

    [Authorize(Roles = ["Owner"])]
    public async Task<bool> DeleteUser([Service] IUserService userService,
        Guid userId,
        CancellationToken ct)
    {
        await userService.DeleteUser(userId, ct);
        return true;
    }

    [Authorize(Roles = ["Owner"])]
    public async Task<bool> UpdateUser([Service] IUserService userService,
        Guid userId,
        string login,
        string password,
        string phoneNumber,
        CancellationToken ct)
    {
        await userService.UpdateUser(userId, login, password, phoneNumber, ct);
        return true;
    }
}