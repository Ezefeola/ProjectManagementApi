using Core.Contracts.DTOs.ProjectUserRoles.Request;
using Core.Contracts.Results;

namespace Core.Contracts.UseCases.ProjectUserRoles
{
    public interface ICreateProjectUserRole
    {
        Task<Result> ExecuteAsync(CreateProjectUserRoleRequestDto requestDto, CancellationToken cancellationToken);
    }
}