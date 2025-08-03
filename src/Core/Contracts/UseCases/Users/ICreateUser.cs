using Core.Contracts.DTOs.Users.Request;
using Core.Contracts.DTOs.Users.Response;
using Core.Contracts.Results;

namespace Core.Contracts.UseCases.Users;
public interface ICreateUser
{
    Task<Result<CreateUserResponseDto>> ExecuteAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken);
}