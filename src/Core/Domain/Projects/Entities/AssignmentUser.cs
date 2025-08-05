using Core.Domain.Abstractions;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class AssignmentUser : Entity
{
    public static class Rules
    {

    }

    private AssignmentUser() { }

    public AssignmentId AssignmentId { get; private set; } = default!;
    public Assignment Assignment { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public User User { get; private set; } = default!;

    internal static AssignmentUser Create(AssignmentId assignmentId, UserId userId)
    {
        AssignmentUser assignmentUser = new()
        {
            AssignmentId = assignmentId,
            UserId = userId
        };
        return assignmentUser;
    }
}