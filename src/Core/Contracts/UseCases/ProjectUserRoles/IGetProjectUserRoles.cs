using Core.Contracts.DTOs.ProjectUserRoles.Response;
using Core.Contracts.Results;

namespace Core.Contracts.UseCases.ProjectUserRoles
{
    public interface IGetProjectUserRoles
    {
        Task<Result<GetProjectUserRolesResponseDto>> ExecuteAsync(CancellationToken cancellationToken);
    }
}