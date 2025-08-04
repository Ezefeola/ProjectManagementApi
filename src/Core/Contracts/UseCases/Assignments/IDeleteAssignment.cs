using Core.Contracts.Results;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.UseCases.Assignments
{
    public interface IDeleteAssignment
    {
        Task<Result> ExecuteAsync(ProjectId projectId, AssignmentId assignmentId, CancellationToken cancellationToken);
    }
}