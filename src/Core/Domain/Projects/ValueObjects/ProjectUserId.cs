using Core.Contracts.Models;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects.ValueObjects;
public sealed record ProjectUserId : ValueObject
{
    private ProjectUserId() { }

    private ProjectUserId(ProjectId projectId, UserId userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    public ProjectId ProjectId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;

    public static ProjectUserId NewId(ProjectId projectId, UserId userId) => new(projectId, userId);
    //public static ProjectUserId NewEfId(ProjectId projectId, UserId userId) => new(projectId, userId);
    public static ProjectUserId Create(ProjectId projectId, UserId userId)
    {

        ProjectUserId projectUserId = new(projectId, userId);
        return projectUserId;
    }
}