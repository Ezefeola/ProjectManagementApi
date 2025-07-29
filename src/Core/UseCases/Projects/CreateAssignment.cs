using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Result;
using Core.Contracts.UnitOfWork;
using Core.Domain.Projects.Entities;
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

        DomainResult<Assignment> assignmentResult = Assignment.Create(
            requestDto.Title,
            requestDto.EstimatedHours,
            requestDto.Status,
            requestDto.Description,
            requestDto.UserId
        );
        if(!assignmentResult.IsSuccess)
        {
            return Result<CreateAssignmentResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                      .WithErrors(assignmentResult.Errors);
        }

        await _unitOfWork.AssignmentRepository.AddAsync(assignmentResult.Value, cancellationToken);
    }
}