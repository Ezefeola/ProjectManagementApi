namespace Core.Contracts.DTOs.ProjectUserRoles.Request;
public sealed record CreateProjectUserRoleRequestDto
{
    public required string Name { get; set; }
}