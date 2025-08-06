using Adapter.SqlServer.Data;
using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.Repositories;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Utilities.QueryOptions;
using Core.Utilities.QueryOptions.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SqlServer.Repositories;
public class AssignmentRepository : GenericRepository<Assignment, AssignmentId>, IAssignmentRepository
{
    public AssignmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentsForProjectAsync(
        ProjectId projectId, 
        GetAssignmentsForProjectRequestDto parametersRequestDto,
        CancellationToken cancellationToken
    )
    {
        return await Query()
                     .AsNoTracking()
                     .Where(x => x.ProjectId == projectId)
                     .OrderBy(x => x.CreatedAt)
                     .ApplyPagination(
                        parametersRequestDto.GetPageIndex(),
                        parametersRequestDto.GetPageSize()
                     )
                     .ToListAsync(cancellationToken);
    }

    public async Task<int> CountByProjectIdAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .CountAsync(x => x.ProjectId == projectId, cancellationToken);
    }
} 