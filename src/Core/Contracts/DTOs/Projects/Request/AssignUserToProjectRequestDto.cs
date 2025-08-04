using Core.Domain.Projects.Entities;

namespace Core.Contracts.DTOs.Projects.Request;
public sealed record AssignUserToProjectRequestDto
{
    public Guid UserId { get; set; }
    public ProjectUser.ProjectUserRoleEnum Role { get; set; }
}