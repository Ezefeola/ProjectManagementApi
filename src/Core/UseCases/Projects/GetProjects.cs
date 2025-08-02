using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Result;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Projects;
using Core.Utilities.Mappers;
using System.Net;

namespace Core.UseCases.Projects;
public class GetProjects : IGetProjects
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjects(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProjectsResponseDto>> ExecuteAsync(GetProjectsRequestDto parametersRequestDto, CancellationToken cancellationToken)
    {
        IEnumerable<Project> projects = await _unitOfWork.ProjectRepository.GetAllAsync(parametersRequestDto, cancellationToken);
        if (!projects.Any())
        {
            return Result<GetProjectsResponseDto>.Failure(HttpStatusCode.NotFound)
                                                 .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        int projectsCount = await _unitOfWork.ProjectRepository.CountAsync(cancellationToken);
        GetProjectsResponseDto responseDto = projects.ToGetProjectsResponseDto(parametersRequestDto, projectsCount);
        return Result<GetProjectsResponseDto>.Success(HttpStatusCode.OK)
                                             .WithPayload(responseDto);
    }
}