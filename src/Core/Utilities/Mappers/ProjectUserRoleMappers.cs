using Core.Contracts.DTOs.ProjectUserRoles.Response;
using Core.Domain.Projects.Entities;

namespace Core.Utilities.Mappers;
public static class ProjectUserRoleMappers
{
    public static ProjectUserRoleResponseDto ToProjectUserRoleResponseDto(this ProjectUserRole projectUserRole)
    {
        return new ProjectUserRoleResponseDto()
        {
            Id = projectUserRole.Id.Value,
            Name = projectUserRole.Name
        };
    }

    public static GetProjectUserRolesResponseDto ToGetProjectUserRolesResponseDto(this IEnumerable<ProjectUserRole> projectUserRoles)
    {
        return new GetProjectUserRolesResponseDto()
        {
            ProjectUserRoleResponseDto = [.. projectUserRoles.Select(projectUserRole => projectUserRole.ToProjectUserRoleResponseDto())]
        };
    }
}