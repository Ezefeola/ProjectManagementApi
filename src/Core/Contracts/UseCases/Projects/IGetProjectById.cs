using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.UseCases.Projects
{
    public interface IGetProjectById
    {
        Task<Result<GetProjectByIdResponseDto>> ExecuteAsync(ProjectId projectId, CancellationToken cancellationToken);
    }
}