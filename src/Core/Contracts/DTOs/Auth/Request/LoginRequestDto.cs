namespace Core.Contracts.DTOs.Auth.Request;
public sealed record LoginRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}