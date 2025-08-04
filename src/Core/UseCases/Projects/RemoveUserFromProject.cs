using Core.Contracts.Models;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using System.Net;

namespace Core.UseCases.Projects;
public class RemoveUserFromProject : IRemoveUserFromProject
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserFromProject(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(
        ProjectId projectId,
        UserId userId,
        CancellationToken cancellationToken
    )
    {
        Project? project = await _unitOfWork.ProjectRepository.GetProjectWithProjectUserAsync(projectId, userId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        DomainResult removeUserFromProjectResult = project.RemoveUser(userId);
        if (!removeUserFromProjectResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors(removeUserFromProjectResult.Errors);
        }

        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.OK);
    }
}