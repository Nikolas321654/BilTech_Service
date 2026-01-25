namespace BShop.Domain.Interfaces.Auth;

public interface IPasswordHasher
{
    public string GeneratePasswordHash(string password);
    public bool VerifyPasswordHash(string password, string hashedPassword);
}