using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;

namespace Adapter.SqlServer.Repositories;
public class AssignmentRepository : GenericRepository<Assignment, AssignmentId>, IAssignmentRepository
{
    public AssignmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}