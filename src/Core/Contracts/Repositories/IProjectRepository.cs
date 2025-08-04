using Core.Contracts.DTOs.Projects.Request;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IProjectRepository : IGenericRepository<Project, ProjectId>
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Project>> GetAllAsync(GetProjectsRequestDto parametersRequestDto, CancellationToken cancellationToken);
    Task<Project?> GetByIdWithAllChildrenAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task<Project?> GetByIdWithAssignmentAsync(ProjectId projectId, AssignmentId assignmentId, CancellationToken cancellationToken);
    Task<Project?> GetByIdWithAssignmentsAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task<Project?> GetProjectWithProjectUsersAsync(ProjectId id, CancellationToken cancellationToken);
}