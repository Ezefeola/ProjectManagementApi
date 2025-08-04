using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using System.Net;

namespace Core.UseCases.Projects;
public class UpdateProjectDetails : IUpdateProjectDetails
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectDetails(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(
        ProjectId projectId,
        UpdateProjectDetailsRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        Project? project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId, cancellationToken);
        if (project is null)
        {
            return Result.Failure(HttpStatusCode.NotFound)
                         .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        DomainResult updateProjectDetailsResult = project.UpdateDetails(
            requestDto.Name,
            requestDto.Description,
            requestDto.StartDate,
            requestDto.EndDate
        );
        if (!updateProjectDetailsResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors(updateProjectDetailsResult.Errors);
        }

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(HttpStatusCode.OK);
    }
}