using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using Core.Utilities.Mappers;
using System.Net;

namespace Core.UseCases.Projects;
public class GetProjectById : IGetProjectById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjectById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProjectByIdResponseDto>> ExecuteAsync(ProjectId projectId, CancellationToken cancellationToken)
    {
        Project? project = await _unitOfWork.ProjectRepository.GetByIdWithAssignmentsAsync(projectId, cancellationToken);
        if (project is null)
        {
            return Result<GetProjectByIdResponseDto>.Failure(HttpStatusCode.NotFound)
                                                    .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        GetProjectByIdResponseDto responseDto = project.ToGetProjectByIdResponseDto();
        return Result<GetProjectByIdResponseDto>.Success(HttpStatusCode.OK)
                                                .WithPayload(responseDto);
    }
}