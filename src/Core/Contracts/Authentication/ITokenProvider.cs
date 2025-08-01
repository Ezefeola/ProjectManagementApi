using Core.Domain.Users;

namespace Core.Contracts.Authentication;
public interface ITokenProvider
{
    string GenerateToken(User user);
}