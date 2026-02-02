using BUser.Api.Models;
using AutoMapper;
using BUser.Domain.Interfaces;
using HotChocolate;
using HotChocolate.Types;

namespace BUser.Api.GraphQl.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class UserMutation
{
    public async Task<User> AddUser([Service] IUserService userService,
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
        return mapper.Map<User>(user);
    }

    public async Task<bool> DeleteUser([Service] IUserService userService,
        Guid userId,
        CancellationToken ct)
    {
        await userService.DeleteUser(userId, ct);
        return true;
    }

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