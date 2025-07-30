using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;

namespace Adapter.SqlServer.Repositories;
public class ProjectCollaboratorRepository : GenericRepository<ProjectUser, ProjectUserId>, IProjectCollaboratorRepository
{
    public ProjectCollaboratorRepository(ApplicationDbContext context) : base(context)
    {
    }
}