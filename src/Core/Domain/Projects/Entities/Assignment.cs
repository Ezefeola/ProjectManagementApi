using Core.Domain.Abstractions;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class Assignment : Entity<AssignmentId>
{
    public static class ColumnNames
    {
        public const string Id = "Id";
        public const string UserId = "UserId";
        public const string Title = "Title";
        public const string Description = "Description";
        public const string EstimatedHours = "EstimatedHours";
        public const string LoggedHours = "LoggedHours";
        public const string Status = "Status";
        public const string ProjectId = "ProjectId";
        public const string CollaboratorId = "CollaboratorId";
    }
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
    public UserId? UserId { get; private set; }
    public User? User { get; private set; }

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
        if(!assignmentStatusResult.IsSuccess) errors.AddRange(assignmentStatusResult.Errors);

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
            UserId = userId
        };
        return DomainResult<Assignment>.Success(assignment);
    }
}