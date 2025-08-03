using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.Models;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using System.Net;

namespace Core.UseCases.Projects;
public class DeleteAssignment : IDeleteAssignment
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAssignment(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(ProjectId projectId, AssignmentId assignmentId, CancellationToken cancellationToken)
    {
        Project? project = await _unitOfWork.ProjectRepository.GetProjectWithAssignmentAsync(
            projectId,
            assignmentId,
            cancellationToken
        );
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

        project.RemoveAssignment(assignmentId);

        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.OK);
    }
}