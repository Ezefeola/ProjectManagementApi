using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Models;
using Core.Contracts.Result;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Abstractions;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using Core.Utilities.Mappers;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace Core.UseCases.Projects;
public class CreateAssignment : ICreateAssignment
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateAssignmentRequestDto> _validator;

    public CreateAssignment(
        IUnitOfWork unitOfWork,
        IValidator<CreateAssignmentRequestDto> validator
    )
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<CreateAssignmentResponseDto>> ExecuteAsync(
        CreateAssignmentRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validatorResult = _validator.Validate(requestDto);
        if (!validatorResult.IsValid)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                      .WithErrors([.. validatorResult.Errors.Select(e => e.ErrorMessage)]);
        }

        ProjectId projectId = ProjectId.Create(requestDto.ProjectId);

        Project? project = await _unitOfWork.ProjectRepository.GetByIdAsync(
            projectId,
            cancellationToken
        );
        if (project is null)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.NotFound)
                                                      .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        UserId? userId = null;
        if (requestDto.UserId.HasValue)
        {
            userId = UserId.Create(requestDto.UserId.Value);
            
            bool userExists = await _unitOfWork.UserRepository.ExistsByIdAsync(userId, cancellationToken);
            if (!userExists)
            {
                return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.NotFound)
                                                          .WithErrors([DomainErrors.UserErrors.USER_NOT_FOUND]);
            }
        }

        DomainResult<Project> addAssignmentResult = project.AddAssignment(
            requestDto.Title,
            requestDto.EstimatedHours,
            requestDto.Status,
            requestDto.Description,
            userId
        );
        if (!addAssignmentResult.IsSuccess)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                      .WithErrors(addAssignmentResult.Errors);
        }

        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.InternalServerError)
                                                      .WithErrors([saveResult.ErrorMessage]);
        }

        CreateAssignmentResponseDto responseDto = project.ToCreateAssignmentResponseDto();
        return Result<CreateAssignmentResponseDto>.Success(HttpStatusCode.Created)
                                                  .WithPayload(responseDto);
    }
}