using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.Models;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using System.Net;

namespace Core.UseCases.Assignments;
public class UpdateAssignmentDetails : IUpdateAssignmentDetails
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAssignmentDetails(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(
        ProjectId projectId,
        AssignmentId assignmentId,
        UpdateAssignmentDetailsRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
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

        DomainResult updateAssignmentDetailsResult = project.UpdateAssignmentDetails(
            assignmentId,
            requestDto.Title,
            requestDto.Description,
            requestDto.EstimatedHours
        );
        if (!updateAssignmentDetailsResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors(updateAssignmentDetailsResult.Errors);
        }

        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.NoContent);
    }
}