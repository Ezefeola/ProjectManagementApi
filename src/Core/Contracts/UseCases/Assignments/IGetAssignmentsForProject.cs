using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.DTOs.Assignment.Response;
using Core.Contracts.Results;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.UseCases.Assignments
{
    public interface IGetAssignmentsForProject
    {
        Task<Result<GetAssignmentsForProjectResponseDto>> ExecuteAsync(ProjectId projectId, GetAssignmentsForProjectRequestDto parametersRequestDto, CancellationToken cancellationToken);
    }
}