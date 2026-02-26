using AutoMapper;
using AutoMapper.QueryableExtensions;
using BUser.Api.Models;
using BUser.Domain.Interfaces;
using HotChocolate.Authorization;

namespace BUser.Api.GraphQl.Query;

[ExtendObjectType(Name = "Query")]
public class UserQuery
{
    [Authorize(Roles = ["Owner"])]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<UserResponse> GetAllUsers([Service] IUserService userService, [Service] IMapper mapper,
        Guid workPlaceId, CancellationToken ct)
    {
        return userService.GetAllUsers(workPlaceId, ct).ProjectTo<UserResponse>(mapper.ConfigurationProvider);
    }

    [Authorize(Roles = ["Owner"])]
    [UseFirstOrDefault]
    [UseProjection]
    public async Task<UserResponse> GetUserById([Service] IUserService userService, [Service] IMapper mapper,
        Guid userId, CancellationToken ct)
    {
        var user = await userService.GetUserById(userId, ct);
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
}