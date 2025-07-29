using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Models;
using Core.Contracts.Result;
using Core.Contracts.UnitOfWork;
using Core.Domain.Abstractions;
using Core.Domain.Projects;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Utilities.Mappers;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace Core.UseCases.Projects;
public class CreateAssignment
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
        ValidationResult validatorResult = await _validator.ValidateAsync(requestDto, cancellationToken);
        if(!validatorResult.IsValid)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                      .WithErrors([..validatorResult.Errors.Select(e => e.ErrorMessage)]);
        }

        DomainResult<ProjectId> projectIdResult = ProjectId.Create(requestDto.ProjectId);
        if(!projectIdResult.IsSuccess)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                      .WithErrors(projectIdResult.Errors);
        }

        Project? project = await _unitOfWork.ProjectRepository.GetByIdAsync(
            projectIdResult.Value,
            cancellationToken
        );
        if(project is null)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.NotFound)
                                                      .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        DomainResult<Project> addAssignmentResult = project.AddAssignment(
            requestDto.Title,
            requestDto.EstimatedHours,
            requestDto.Status,
            requestDto.Description,
            requestDto.UserId
        );
        if(!addAssignmentResult.IsSuccess)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                      .WithErrors(addAssignmentResult.Errors);
        }

        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if(!saveResult.IsSuccess)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.InternalServerError)
                                                      .WithErrors([saveResult.ErrorMessage]);
        }

        CreateAssignmentResponseDto responseDto = project.ToCreateAssignmentResponseDto();
        return Result<CreateAssignmentResponseDto>.Success(HttpStatusCode.Created)
                                                  .WithPayload(responseDto);
    }
}