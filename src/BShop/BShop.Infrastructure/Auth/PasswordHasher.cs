using BShop.Domain.Interfaces;
using BShop.Domain.Interfaces.Auth;

namespace BShop.Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string GeneratePasswordHash(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool VerifyPasswordHash(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}