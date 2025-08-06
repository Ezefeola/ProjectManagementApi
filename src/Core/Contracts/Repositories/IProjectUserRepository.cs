using Core.Contracts.DTOs.Projects.Response;
using Core.Domain.Projects;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IProjectUserRepository : IGenericRepository<ProjectUser>
{
    Task<int> CountProjectsForUserAsync(UserId userId, CancellationToken cancellationToken);
    Task<IEnumerable<Project>> GetProjectsForUserAsync(UserId userId, CancellationToken cancellationToken);
    Task<IEnumerable<GetUserForProjectResponseDto>> GetUsersForProjectAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task<bool> IsUserAssignedToProjectAsync(ProjectId projectId, UserId userId, CancellationToken cancellationToken);
}