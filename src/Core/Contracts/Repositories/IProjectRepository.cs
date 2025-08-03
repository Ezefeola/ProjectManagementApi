using Core.Contracts.DTOs.Projects.Request;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IProjectRepository : IGenericRepository<Project, ProjectId>
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Project>> GetAllAsync(GetProjectsRequestDto parametersRequestDto, CancellationToken cancellationToken);
    Task<Project?> GetProjectWithAssignmentAsync(ProjectId projectId, AssignmentId assignmentId, CancellationToken cancellationToken);
}