using Core.Contracts.Results;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.UseCases.Projects
{
    public interface IRemoveUserFromProject
    {
        Task<Result> ExecuteAsync(ProjectId projectId, UserId userId, CancellationToken cancellationToken);
    }
}