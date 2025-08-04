using Core.Domain.Abstractions;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class ProjectUser : Entity
{
    public enum ProjectUserRoleEnum
    {
        Backend_Developer = 1,
        Frontend_Developer = 2,
        QA = 3,
        PM = 4
    }
    private ProjectUser() { }

    public ProjectId ProjectId { get; private set; } = default!;
    public Project Project { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public User User { get; private set; } = default!;
    public ProjectUserRoleEnum Role { get; set; }

    public static ProjectUser Create(ProjectId projectId, UserId userId, ProjectUserRoleEnum role)
    {
        return new ProjectUser()
        {
            ProjectId = projectId,
            UserId = userId,
            Role = role
        };
    }
}