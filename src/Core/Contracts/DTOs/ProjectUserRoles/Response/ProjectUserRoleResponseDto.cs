namespace Core.Contracts.DTOs.ProjectUserRoles.Response;
public sealed record ProjectUserRoleResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}