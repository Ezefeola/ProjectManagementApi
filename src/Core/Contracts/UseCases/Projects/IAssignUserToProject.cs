using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.Results;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.UseCases.Projects
{
    public interface IAssignUserToProject
    {
        Task<Result> ExecuteAsync(ProjectId projectId, AssignUserToProjectRequestDto requestDto, CancellationToken cancellationToken);
    }
}