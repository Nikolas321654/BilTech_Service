using System.Security.Claims;

namespace BShop.GraphQL;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetWorkPlaceId(this ClaimsPrincipal principal)
    {
        var claimValue = principal.FindFirst("workPlaceId")?.Value;
        return !Guid.TryParse(claimValue, out var shopId)
            ? throw new GraphQLException("WorkPlaceId (shopId) is missing or invalid in JWT token.")
            : shopId;
    }

    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var claimValue = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return !Guid.TryParse(claimValue, out var ownerId)
            ? throw new GraphQLException("UserId is missing or invalid in JWT token.")
            : ownerId;
    }
}