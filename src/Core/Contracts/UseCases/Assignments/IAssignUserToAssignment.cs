using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.Results;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.UseCases.Assignments
{
    public interface IAssignUserToAssignment
    {
        Task<Result> ExecuteAsync(ProjectId projectId, AssignmentId assignmentId, AssignUserToAssignmentRequestDto requestDto, CancellationToken cancellationToken);
    }
}