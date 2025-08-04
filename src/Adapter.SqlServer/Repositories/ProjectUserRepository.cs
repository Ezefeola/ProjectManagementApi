using Adapter.SqlServer.Data;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Repositories;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SqlServer.Repositories;
public class ProjectUserRepository : GenericRepository<ProjectUser>, IProjectUserRepository
{
    public ProjectUserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsUserAssignedToProjectAsync(ProjectId projectId, UserId userId, CancellationToken cancellationToken)
    {
        return await Query()
                     .AnyAsync(x => 
                        x.ProjectId == projectId && 
                        x.UserId == userId, 
                        cancellationToken
                     );
    }
    public async Task<IEnumerable<GetUserForProjectResponseDto>> GetUsersForProjectAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        return await Query()
                     .Where(x => x.ProjectId == projectId)
                     .Select(x => new GetUserForProjectResponseDto()
                     {
                         UserId = x.UserId.Value,
                         FirstName = x.User.FullName.FirstName,
                         LastName = x.User.FullName.LastName,
                         EmailAddress = x.User.EmailAddress.Value,
                         Role = x.Role,
                     })
                     .ToListAsync(cancellationToken);
    }
}