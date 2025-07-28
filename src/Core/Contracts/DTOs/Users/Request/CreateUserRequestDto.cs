using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.DTOs.Users.Request;
public sealed record CreateUserRequestDto
{
    public required UserRole.UserRolesEnum Role { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}