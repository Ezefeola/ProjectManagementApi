using System.Security.Claims;
using Core.Contracts.Authentication;
using Microsoft.AspNetCore.Http;

namespace Adapter.SqlServer.Authentication;
public class UserInfo : IUserInfo
{
    public UserInfo(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal? userClaims = httpContextAccessor.HttpContext?.User;
        if (userClaims == null)
        {
            throw new InvalidOperationException("User is not authenticated or is missing.");
        }

        string? sub = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? userClaims.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(sub))
        {
            throw new InvalidOperationException("User ID not found in the token.");
        }

        UserId = Guid.Parse(sub);
    }

    public Guid UserId { get; }
}