using Core.Contracts.DTOs.Projects.Request;
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
public class AssignUserToProject : IAssignUserToProject
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserToProject(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(
        ProjectId projectId,
        AssignUserToProjectRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        UserId userId = UserId.Create(requestDto.UserId);

        Project? project = await _unitOfWork.ProjectRepository.GetProjectWithProjectUsersAsync(projectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        bool userExists = await _unitOfWork.UserRepository.ExistsByIdAsync(userId, cancellationToken);
        if (!userExists)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.UserErrors.USER_DOES_NOT_EXIST]);
        }

        DomainResult assignUserToProjectResult = project.AssignUser(userId, requestDto.Role);
        if (!assignUserToProjectResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors(assignUserToProjectResult.Errors);
        }

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(HttpStatusCode.NoContent);
    }
}