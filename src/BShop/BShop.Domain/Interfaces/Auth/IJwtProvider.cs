using BShop.Domain.Model;

namespace BShop.Domain.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateJwtToken(Worker worker);
}