using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IProjectRepository : IGenericRepository<Project, ProjectId>
{
}