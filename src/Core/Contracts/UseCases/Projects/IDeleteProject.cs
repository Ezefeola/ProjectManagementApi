using Core.Contracts.Results;
using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.UseCases.Projects
{
    public interface IDeleteProject
    {
        Task<Result> ExecuteAsync(ProjectId projectId, CancellationToken cancellationToken);
    }
}