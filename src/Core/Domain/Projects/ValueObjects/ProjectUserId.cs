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
    public static ProjectUserId NewEfId(ProjectId projectId, UserId userId) => new(projectId, userId);
    public static DomainResult<ProjectUserId> Create(Guid projectId, Guid userId)
    {
        List<string> errors = [];

        DomainResult<ProjectId> projectIdResult = ProjectId.Create(projectId);
        if (!projectIdResult.IsSuccess) errors.AddRange(projectIdResult.Errors);

        DomainResult<UserId> userIdResult = UserId.Create(userId);
        if (!userIdResult.IsSuccess) errors.AddRange(userIdResult.Errors);

        if (errors.Count > 0)
        {
            return DomainResult<ProjectUserId>.Failure()
                                                      .WithErrors(errors);
        }

        ProjectUserId projectCollaboratorId = new(projectIdResult.Value, userIdResult.Value);
        return DomainResult<ProjectUserId>.Success()
                                                  .WithValue(projectCollaboratorId);
    }
}