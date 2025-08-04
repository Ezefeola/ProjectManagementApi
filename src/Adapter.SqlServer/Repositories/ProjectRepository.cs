using Adapter.SqlServer.Data;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.Repositories;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using Core.Utilities.QueryOptions;
using Core.Utilities.QueryOptions.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SqlServer.Repositories;
public class ProjectRepository : GenericRepository<Project, ProjectId>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Project>> GetAllAsync(GetProjectsRequestDto parametersRequestDto, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .ApplyPagination(parametersRequestDto.GetPageIndex(), parametersRequestDto.GetPageSize())
                     .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdWithAllChildrenAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        return await Query()
                     .Where(x => x.Id == projectId)
                     .Include(x => x.Assignments)
                     .Include(x => x.ProjectUsers)
                     .AsSplitQuery()
                     .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdWithAssignmentsAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        return await Query()
                     .Include(x => x.Assignments)
                     .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken);
    }

    public async Task<Project?> GetByIdWithAssignmentAsync(ProjectId projectId, AssignmentId assignmentId, CancellationToken cancellationToken)
    {
        return await Query()
                     .Include(x => x.Assignments
                        .Where(a => a.Id == assignmentId)
                     )
                     .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken);
    }

    public async Task<Project?> GetProjectWithProjectUsersAsync(ProjectId id, CancellationToken cancellationToken)
    {
        return await Query()
                     .Where(x => x.Id == id)
                     .Include(p => p.ProjectUsers)
                     .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Project?> GetProjectWithProjectUserAsync(ProjectId projectId, UserId userId, CancellationToken cancellationToken)
    {
        return await Query()
                     .Where(x => x.Id == projectId)
                     .Include(x => x.ProjectUsers
                        .Where(x => x.UserId == userId)
                     )
                     .FirstOrDefaultAsync(cancellationToken); 
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .CountAsync(cancellationToken);
    }
}