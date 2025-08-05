using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IAssignmentUserRepository : IGenericRepository<AssignmentUser>
{
    Task<bool> IsUserAssignedToAssignmentAsync(AssignmentId assignmentId, UserId userId, CancellationToken cancellationToken);
}