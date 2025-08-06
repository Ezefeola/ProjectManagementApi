using Core.Contracts.DTOs.ProjectUserRoles.Response;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.ProjectUserRoles;
using Core.Domain.Common;
using Core.Domain.Projects.Entities;
using Core.Utilities.Mappers;
using System.Net;

namespace Core.UseCases.ProjectUserRoles;
public class GetProjectUserRoles : IGetProjectUserRoles
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjectUserRoles(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProjectUserRolesResponseDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        IEnumerable<ProjectUserRole> projectUserRoles = await _unitOfWork.ProjectUserRoleRepository.GetAllAsync(cancellationToken);
        if (!projectUserRoles.Any())
        {
            return Result<GetProjectUserRolesResponseDto>.Failure(HttpStatusCode.NotFound)
                                                         .WithErrors([DomainErrors.ProjectUserRoleErrors.PROJECT_USER_ROLES_NOT_FOUND]);
        }

        GetProjectUserRolesResponseDto responseDto = projectUserRoles.ToGetProjectUserRolesResponseDto();
        return Result<GetProjectUserRolesResponseDto>.Success(HttpStatusCode.OK)
                                                     .WithPayload(responseDto);
    }
}