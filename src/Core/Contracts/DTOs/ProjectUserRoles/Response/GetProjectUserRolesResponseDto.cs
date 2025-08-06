namespace Core.Contracts.DTOs.ProjectUserRoles.Response;
public sealed record GetProjectUserRolesResponseDto
{
    public IReadOnlyCollection<ProjectUserRoleResponseDto> ProjectUserRoleResponseDto { get; set; } = default!;
}