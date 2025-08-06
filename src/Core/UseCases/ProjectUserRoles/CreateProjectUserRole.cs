using Core.Contracts.DTOs.ProjectUserRoles.Request;
using Core.Contracts.Models;
using Core.Contracts.Results;
using Core.Contracts.UnitOfWork;
using Core.Contracts.UseCases.ProjectUserRoles;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects.Entities;
using System.Net;

namespace Core.UseCases.ProjectUserRoles;
public class CreateProjectUserRole : ICreateProjectUserRole
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectUserRole(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(
        CreateProjectUserRoleRequestDto requestDto,
        CancellationToken cancellationToken
    )
    {
        bool nameExists = await _unitOfWork.ProjectUserRoleRepository.ExistsByNameAsync(requestDto.Name, cancellationToken);
        if (nameExists)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([DomainErrors.ProjectUserRoleErrors.NAME_ALREAD_EXISTS]);
        }

        DomainResult<ProjectUserRole> projectUserRoleResult = ProjectUserRole.Create(requestDto.Name);
        if (!projectUserRoleResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors(projectUserRoleResult.Errors);
        }

        await _unitOfWork.ProjectUserRoleRepository.AddAsync(projectUserRoleResult.Value, cancellationToken);
        SaveResult saveResult = await _unitOfWork.CompleteAsync(cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Failure(HttpStatusCode.BadRequest)
                         .WithErrors([saveResult.ErrorMessage]);
        }

        return Result.Success(HttpStatusCode.Created);
    }
}