using Core.Contracts.DTOs.Projects.Response;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IProjectUserRepository : IGenericRepository<ProjectUser>
{
    Task<IEnumerable<GetUserForProjectResponseDto>> GetUsersForProjectAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task<bool> IsUserAssignedToProjectAsync(ProjectId projectId, UserId userId, CancellationToken cancellationToken);
}