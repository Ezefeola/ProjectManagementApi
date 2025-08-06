using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.UseCases.Projects
{
    public interface IGetProjectsForUser
    {
        Task<Result<GetProjectsForUserResponseDto>> ExecuteAsync(UserId userId, GetProjectsForUserRequestDto parametersRequestDto, CancellationToken cancellationToken);
    }
}