using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;

namespace Core.Domain.Projects.ValueObjects;
public sealed record AssignmentStatus : ValueObject
{
    private AssignmentStatus() { }

    public enum AssignmentStatusEnum 
    {
        ToDo = 1,
        InProgress = 2,
        Done = 3
    }

    public AssignmentStatusEnum Value { get; set; }

    public static DomainResult<AssignmentStatus> Create(AssignmentStatusEnum value)
    {
        if (!Enum.IsDefined(value))
        {
            return DomainResult<AssignmentStatus>.Failure([DomainErrors.AssignmentErrors.INVALID_ASSIGNMENT_STATUS]);
        }

        AssignmentStatus assignmentStatus = new()
        {
            Value = value
        };
        return DomainResult<AssignmentStatus>.Success(assignmentStatus);
    }
}