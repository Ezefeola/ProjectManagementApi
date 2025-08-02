using Core.Contracts.DTOs.Users.Response;
using Core.Domain.Users;

namespace Core.Utilities.Mappers;
public static class UserMappers
{
    public static UserResponseDto ToUserResponseDto(this User user)
    {
        return new UserResponseDto
        {
            Id = user.Id.Value,
            FirstName = user.FullName.FirstName,
            LastName = user.FullName.LastName,
            Email = user.EmailAddress.Value,
            Role = user.Role.Value.ToString(),
        };
    }

    public static CreateUserResponseDto ToCreateUserResponseDto(this User user)
    {
        return new CreateUserResponseDto()
        {
            UserResponseDto = user.ToUserResponseDto(),
        };
    }
}