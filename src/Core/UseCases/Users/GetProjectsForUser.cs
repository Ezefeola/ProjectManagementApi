using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Common;
using Core.Domain.Projects;
using Core.Domain.Users.ValueObjects;
using Core.Utilities.Mappers;
using System.Net;

namespace Core.UseCases.Users;
public class GetProjectsForUser : IGetProjectsForUser
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjectsForUser(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProjectsForUserResponseDto>> ExecuteAsync(
        UserId userId,
        GetProjectsForUserRequestDto parametersRequestDto,
        CancellationToken cancellationToken
    )
    {
        bool userExists = await _unitOfWork.UserRepository.ExistsByIdAsync(userId, cancellationToken);
        if (!userExists)
        {
            return Result<GetProjectsForUserResponseDto>.Failure(HttpStatusCode.NotFound)
                                                        .WithErrors([DomainErrors.UserErrors.USER_NOT_FOUND]);
        }

        IEnumerable<Project> projects = await _unitOfWork.ProjectUserRepository.GetProjectsForUserAsync(userId, cancellationToken);
        if (!projects.Any())
        {
            return Result<GetProjectsForUserResponseDto>.Failure(HttpStatusCode.NotFound)
                                                        .WithErrors([DomainErrors.ProjectErrors.PROJECTS_NOT_FOUND]);
        }

        int totalProjectsForUser = await _unitOfWork.ProjectUserRepository.CountProjectsForUserAsync(userId, cancellationToken);
        GetProjectsForUserResponseDto responseDto = projects.ToGetProjectsForUserResponseDto(parametersRequestDto, totalProjectsForUser);

        return Result<GetProjectsForUserResponseDto>.Success(HttpStatusCode.OK)
                                                    .WithPayload(responseDto);
    }
}