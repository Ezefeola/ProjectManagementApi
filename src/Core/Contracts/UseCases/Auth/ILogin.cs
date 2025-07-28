using Core.Contracts.DTOs.Auth.Request;
using Core.Contracts.DTOs.Auth.Response;
using Core.Contracts.Result;

namespace Core.Contracts.UseCases.Auth
{
    public interface ILogin
    {
        Task<Result<LoginResponseDto>> ExecuteAsync(LoginRequestDto requestDto, CancellationToken cancellationToken);
    }
}