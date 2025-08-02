using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Contracts.Authentication;
using Core.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Adapter.SqlServer.Authentication;
public class TokenProvider(IConfiguration _configuration) : ITokenProvider
{
    public string GenerateToken(User user)
    {
        if (user.Id.Value == Guid.Empty)
        {
            throw new Exception("The user Id is not valid.");
        }
        if (user.Role is null)
        {
            throw new Exception("The user Role is not valid.");
        }

        string? key = _configuration["Jwt:Key"]!;
        string? issuer = _configuration["Jwt:Issuer"]!;
        string? audience = _configuration["Jwt:Audience"]!;
        int expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"]!);
        DateTime? tokenExpires = DateTime.UtcNow.AddMinutes(expireMinutes);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = [
            new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress.Value),
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, user.Role.Value.ToString())
        ];

        JwtSecurityToken? token = new(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: tokenExpires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}