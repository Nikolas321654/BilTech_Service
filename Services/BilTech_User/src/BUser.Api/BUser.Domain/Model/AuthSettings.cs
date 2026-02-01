namespace BUser.Domain.Model;

public class AuthSettings
{
    public TimeSpan TokenExpiration { get; set; }
    public string SecretKey { get; set; } = string.Empty;
}