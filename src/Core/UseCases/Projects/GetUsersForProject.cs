using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Projects.ValueObjects;
using System.Net;

namespace Core.UseCases.Projects;
public class GetUsersForProject : IGetUsersForProject
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersForProject(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<GetUserForProjectResponseDto>>> ExecuteAsync(
        ProjectId projectId,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<GetUserForProjectResponseDto> responseDtos = await _unitOfWork.ProjectUserRepository.GetUsersForProjectAsync(
            projectId,
            cancellationToken
        );
        if (!responseDtos.Any())
        {
            return Result<IEnumerable<GetUserForProjectResponseDto>>.Failure(HttpStatusCode.NotFound)
                                                                    .WithErrors([DomainErrors.ProjectErrors.PROJECT_NOT_FOUND]);
        }

        return Result<IEnumerable<GetUserForProjectResponseDto>>.Success(HttpStatusCode.OK)
                                                                .WithPayload(responseDtos);
    }
}