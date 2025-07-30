using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Projects.Entities;

namespace Adapter.SqlServer.Repositories;
public class ProjectUserRepository : GenericRepository<ProjectUser>, IProjectUserRepository
{
    public ProjectUserRepository(ApplicationDbContext context) : base(context)
    {
    }
}