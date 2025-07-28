namespace Core.Contracts.DTOs.Auth.Response;
public sealed record LoginResponseDto
{
    public string Token { get; set; } = default!;
}