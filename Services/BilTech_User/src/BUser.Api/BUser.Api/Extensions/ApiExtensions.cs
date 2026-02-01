using System.Text;
using BUser.Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BUser.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("AuthSettings").Get<AuthSettings>();
        var jwtSecretKey = jwtSettings?.SecretKey;

        if (string.IsNullOrEmpty(jwtSecretKey))
        {
            throw new Exception("JWT Secret Key is not configured!");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecretKey!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();
    }
}