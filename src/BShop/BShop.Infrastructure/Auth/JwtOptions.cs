namespace BShop.Infrastructure.Auth;

public class JwtOptions
{
    public string SecretKey { get; set; } = String.Empty;
    public int ExpirationInHours { get; set; }
}