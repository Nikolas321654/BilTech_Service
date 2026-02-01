using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BUser.Domain.Interfaces;
using BUser.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BUser.Infrastructure;

public class JwtProvider(IOptions<AuthSettings> authSettings) : IJwtProvider
{
    public string GenerateJwtToken(UserEntity user)
    {
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256Signature);

        Claim[] claims =
        [
            new("userId", user.Id.ToString()),
            new("role", user.Role.ToString())
        ];
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.Add(authSettings.Value.TokenExpiration));

        var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return jwtTokenString;
    }
}