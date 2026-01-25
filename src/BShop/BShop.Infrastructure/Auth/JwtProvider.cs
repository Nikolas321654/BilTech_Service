using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BShop.Domain;
using BShop.Domain.Interfaces.Auth;
using BShop.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BShop.Infrastructure.Auth;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateJwtToken(Worker worker)
    {
        Claim[] claims =
        [
            new(ClaimTypes.NameIdentifier, worker.Id.ToString()),
            new(ClaimTypes.Role, nameof(worker.Role))
        ];
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpirationInHours));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}