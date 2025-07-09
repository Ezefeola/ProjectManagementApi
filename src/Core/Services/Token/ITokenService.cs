using Core.Domain.Users;

namespace Core.Services.Token;
public interface ITokenService
{
    string GenerateToken(User user);
}