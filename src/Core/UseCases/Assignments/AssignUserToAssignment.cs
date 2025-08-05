using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.Models;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using System.Net;

namespace Core.UseCases.Assignments;
public class AssignUserToAssignment : IAssignUserToAssignment
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserToAssignment(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(
        ProjectId projectId,
        AssignmentId assignmentId,
        AssignUserToAssignmentRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        UserId userId = UserId.Create(requestDto.UserId);

        bool userExists = await _unitOfWork.UserRepository.ExistsByIdAsync(userId, cancellationToken);
        if(!userExists)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.UserErrors.USER_NOT_FOUND]);
        }

        Project? project = await _unitOfWork.ProjectRepository.GetByIdWithAssignmentAsync(projectId, assignmentId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }
        if (!project.Assignments.Any())
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.AssignmentErrors.ASSIGNMENT_NOT_FOUND]);
        }

        bool userAlreadyAssigned = await _unitOfWork.AssignmentUserRepository.IsUserAssignedToAssignmentAsync(
            assignmentId,
            userId,
            cancellationToken
        );
        DomainResult assignUserToAssignmentResult = project.AssignUserToAssignment(assignmentId, userId, userAlreadyAssigned);
        if (!assignUserToAssignmentResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors(assignUserToAssignmentResult.Errors);
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