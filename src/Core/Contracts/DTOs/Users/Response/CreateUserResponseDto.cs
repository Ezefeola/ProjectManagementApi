namespace Core.Contracts.DTOs.Users.Response;
public sealed record CreateUserResponseDto
{
    public UserResponseDto UserResponseDto { get; set; } = default!;
}