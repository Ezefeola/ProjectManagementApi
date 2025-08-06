using Core.Contracts.DTOs.Assignment.Request;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IAssignmentRepository : IGenericRepository<Assignment, AssignmentId>
{
    Task<int> CountByProjectIdAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task<IEnumerable<Assignment>> GetAssignmentsForProjectAsync(
        ProjectId projectId, 
        GetAssignmentsForProjectRequestDto parametersRequestDto,
        CancellationToken cancellationToken
    );
}