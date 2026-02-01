using BUser.Domain.Model;
using Microsoft.Extensions.Options;

namespace BUser.Domain.Interfaces;

public interface IJwtProvider
{
    public string GenerateJwtToken(UserEntity user);
}