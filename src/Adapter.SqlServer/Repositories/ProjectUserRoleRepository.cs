using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SqlServer.Repositories;
public class ProjectUserRoleRepository : GenericRepository<ProjectUserRole, ProjectUserRoleId>, IProjectUserRoleRepository
{
    public ProjectUserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProjectUserRole>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
    }
    public async Task<bool> ProjectUserRoleExistsAsync(ProjectUserRoleId projectUserRoleId, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .AnyAsync(x => x.Id == projectUserRoleId, cancellationToken);
    }
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .AnyAsync(x => x.Name == name, cancellationToken);
    }
}