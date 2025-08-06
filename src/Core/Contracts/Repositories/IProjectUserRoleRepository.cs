using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IProjectUserRoleRepository : IGenericRepository<ProjectUserRole, ProjectUserRoleId>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task<IEnumerable<ProjectUserRole>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> ProjectUserRoleExistsAsync(ProjectUserRoleId projectUserRoleId, CancellationToken cancellationToken);
}