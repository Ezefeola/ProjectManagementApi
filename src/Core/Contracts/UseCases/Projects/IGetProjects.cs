using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;

namespace Core.Contracts.UseCases.Projects
{
    public interface IGetProjects
    {
        Task<Result<GetProjectsResponseDto>> ExecuteAsync(GetProjectsRequestDto parametersRequestDto, CancellationToken cancellationToken);
    }
}