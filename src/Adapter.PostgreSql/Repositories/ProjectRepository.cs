using Adapter.SqlServer.Data;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.Repositories;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
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

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .CountAsync(cancellationToken);
    }
}