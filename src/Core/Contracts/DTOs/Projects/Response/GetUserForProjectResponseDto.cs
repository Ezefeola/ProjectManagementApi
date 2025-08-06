using Core.Contracts.DTOs.ProjectUserRoles.Response;

namespace Core.Contracts.DTOs.Projects.Response;
public sealed record GetUserForProjectResponseDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public ProjectUserRoleResponseDto ProjectUserRoleResponseDto { get; set; } = default!;
}