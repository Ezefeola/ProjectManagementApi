using Core.Contracts.DTOs.Projects.Request;
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
        ProjectUserRoleId projectUserRoleId = ProjectUserRoleId.Create(requestDto.ProjectUserRoleId);

        bool userExists = await _unitOfWork.UserRepository.ExistsByIdAsync(userId, cancellationToken);
        if (!userExists)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.UserErrors.USER_DOES_NOT_EXIST]);
        }

        bool projectUserRoleExists = await _unitOfWork.ProjectUserRoleRepository.ProjectUserRoleExistsAsync(projectUserRoleId, cancellationToken);
        if (!projectUserRoleExists)
        {
            return Result.Failure(HttpStatusCode.BadRequest).WithErrors(["Project user role not found."]);
        }

        Project? project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        bool isUserAssigned = await _unitOfWork.ProjectUserRepository.IsUserAssignedToProjectAsync(projectId, userId, cancellationToken);
        DomainResult assignUserToProjectResult = project.AssignUser(userId, projectUserRoleId, isUserAssigned);
        if (!assignUserToProjectResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors(assignUserToProjectResult.Errors);
        }

       SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if(!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.NoContent);
    }
}