using Core.Domain.Abstractions;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class Assignment : Entity<AssignmentId>
{
    public static class Rules
    {
        public const int TITLE_MAX_LENGTH = 255;
        public const int DESCRIPTION_MAX_LENGTH = 1000;
        public const decimal ESTIMATED_HOURS_MIN = 0.1m;
        public const decimal LOGGED_HOURS_MIN = 0.0m;
    }

    private Assignment() { }

    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public decimal? EstimatedHours { get; private set; } = default!;
    public decimal? LoggedHours { get; private set; } = default!;
    public AssignmentStatus Status { get; private set; } = default!;
    public ProjectId ProjectId { get; private set; } = default!;
    public Project Project { get; private set; } = default!;
    private readonly List<AssignmentUser> _assignmentUsers = [];
    public IReadOnlyList<AssignmentUser> AssignmentUsers => _assignmentUsers;

    internal static DomainResult<Assignment> Create(
        ProjectId projectId,
        string title,
        decimal? estimatedHours,
        AssignmentStatus.AssignmentStatusEnum status,
        string? description,
        UserId? userId
    )
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(title)) errors.Add(DomainErrors.AssignmentErrors.TITLE_NOT_EMPTY);

        DomainResult<AssignmentStatus> assignmentStatusResult = AssignmentStatus.Create(status);
        if (!assignmentStatusResult.IsSuccess) errors.AddRange(assignmentStatusResult.Errors);

        if (errors.Count > 0)
        {
            return DomainResult<Assignment>.Failure(errors);
        }

        Assignment assignment = new()
        {
            Id = AssignmentId.NewId(),
            ProjectId = projectId,
            Title = title,
            Description = description,
            EstimatedHours = estimatedHours,
            Status = assignmentStatusResult.Value,
        };

        if (userId is not null)
        {
            assignment.AssignUser(userId, false);
        }

        return DomainResult<Assignment>.Success(assignment);
    }

    public DomainResult AssignUser(UserId userId, bool userAlreadyAssigned) 
    {
        if (userAlreadyAssigned)
        {
            return DomainResult.Failure([DomainErrors.AssignmentErrors.USER_ALREADY_ASSIGNED]);
        }

        AssignmentUser assignmentUser = AssignmentUser.Create(Id, userId);
        _assignmentUsers.Add(assignmentUser);

        return DomainResult.Success();
    }


    internal DomainResult UpdateDetails(
        string? title,
        string? description,
        decimal? estimatedHours
    )
    {
        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(title) && Title != title)
        {
            Title = title;
            isUpdated = true;
        }

        if (!string.IsNullOrWhiteSpace(description) && Description != description)
        {
            Description = description;
            isUpdated = true;
        }

        if (estimatedHours is not null && EstimatedHours != estimatedHours)
        {
            EstimatedHours = estimatedHours;
            isUpdated = true;
        }

        if (isUpdated)
        {
            MarkAsUpdated();
            return DomainResult.Success()
                               .WasUpdated();
        }

        return DomainResult.Success();
    }

    internal DomainResult ChangeStatus(AssignmentStatus.AssignmentStatusEnum status)
    {
        DomainResult<AssignmentStatus> assignmentStatusResult = AssignmentStatus.Create(status);
        if (!assignmentStatusResult.IsSuccess)
        {
            return DomainResult.Failure(assignmentStatusResult.Errors);
        }

        if(Status == assignmentStatusResult.Value)
        {
            return DomainResult.Success();
        }

        Status = assignmentStatusResult.Value;
        MarkAsUpdated();

        return DomainResult.Success()
                           .WasUpdated();
    }
}