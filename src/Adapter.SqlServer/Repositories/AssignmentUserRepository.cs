using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SqlServer.Repositories;
public class AssignmentUserRepository : GenericRepository<AssignmentUser>, IAssignmentUserRepository
{
    public AssignmentUserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsUserAssignedToAssignmentAsync(AssignmentId assignmentId, UserId userId, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .AnyAsync(au => au.AssignmentId == assignmentId && au.UserId == userId, cancellationToken);

    }
}