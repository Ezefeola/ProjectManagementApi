using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects;
using Core.Utilities.Mappers;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace Core.UseCases.Projects;
public class CreateProject : ICreateProject
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProjectRequestDto> _validator;

    public CreateProject(
        IUnitOfWork unitOfWork,
        IValidator<CreateProjectRequestDto> validator
    )
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<CreateProjectResponseDto>> ExecuteAsync(
        CreateProjectRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validatorResult = _validator.Validate(requestDto);
        if (!validatorResult.IsValid)
        {
            return Result<CreateProjectResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                   .WithErrors([.. validatorResult.Errors.Select(e => e.ErrorMessage)]);
        }

        DomainResult<Project> projectResult = Project.Create(
            requestDto.Name,
            requestDto.StartDate,
            requestDto.EndDate,
            requestDto.Status
        );
        if (!projectResult.IsSuccess)
        {
            return Result<CreateProjectResponseDto>.Failure(HttpStatusCode.BadRequest)
                                                   .WithErrors(projectResult.Errors);
        }

        await _unitOfWork.ProjectRepository.AddAsync(projectResult.Value, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        CreateProjectResponseDto responseDto = projectResult.Value.ToCreateProjectResponseDto();
        return Result<CreateProjectResponseDto>.Success(HttpStatusCode.Created)
                                               .WithPayload(responseDto);
    }
}