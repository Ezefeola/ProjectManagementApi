namespace Core.Contracts.DTOs.Projects.Request;
public sealed record AssignUserToProjectRequestDto
{
    public Guid UserId { get; set; }
    public Guid ProjectUserRoleId { get; set; }
}