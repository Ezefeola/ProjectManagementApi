using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;

namespace Adapter.SqlServer.Repositories;
public class ProjectRepository : GenericRepository<Project, ProjectId>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context) : base(context)
    {
    }
}