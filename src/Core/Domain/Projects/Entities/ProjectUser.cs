using Core.Domain.Abstractions;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class ProjectUser : Entity
{
    private ProjectUser() { }

    public ProjectId ProjectId { get; private set; } = default!;
    public Project Project { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public User User { get; private set; } = default!;
    public ProjectUserRoleId ProjectUserRoleId { get; private set; } = default!;
    public ProjectUserRole ProjectUserRole { get; private set; } = default!;

    public static ProjectUser Create(ProjectId projectId, UserId userId, ProjectUserRoleId projectUserRoleId)
    {
        return new ProjectUser()
        {
            ProjectId = projectId,
            UserId = userId,
            ProjectUserRoleId = projectUserRoleId
        };
    }
}