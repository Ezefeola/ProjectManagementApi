using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IAssignmentRepository : IGenericRepository<Assignment, AssignmentId>
{
}