using AutoMapper;
using AutoMapper.QueryableExtensions;
using BUser.Api.Models;
using BUser.Domain.Interfaces;
using HotChocolate.Authorization;

namespace BUser.Api.GraphQl.Query;

[Authorize]
[ExtendObjectType(Name = "Query")]
public class UserQuery
{
    [Authorize(Roles = ["Owner"])]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetAllUsers([Service] IUserService userService, [Service] IMapper mapper,
        Guid workPlaceId, CancellationToken ct)
    {
        return userService.GetAllUsers(workPlaceId, ct).ProjectTo<User>(mapper.ConfigurationProvider);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public async Task<User> GetUserById([Service] IUserService userService, [Service] IMapper mapper,
        Guid userId, CancellationToken ct)
    {
        var user = await userService.GetUserById(userId, ct);
        return mapper.Map<User>(user);
    }
}