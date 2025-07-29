using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;

namespace Core.Domain.Projects;
public sealed class Project : AggregateRoot<ProjectId>
{
    public static class ColumnNames
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string Status = "Status";
    }
    public static class Rules 
    {
        public const int NAME_MAX_LENGTH = 255;
        public const int STATUS_MAX_LENGTH = 255;
    }

    private Project() { }

    public string Name { get; private set; } = default!;
    public DateRange ProjectPeriod { get; private set; } = default!;
    public ProjectStatus Status { get; private set; } = default!;
    public List<Assignment> Assignments { get; private set; } = [];
    public List<User> Users { get; private set; } = [];
    public List<ProjectUser> ProjectUsers { get; set; } = [];

    public static DomainResult<Project> Create(
        string name,
        DateTime startDate,
        DateTime endDate,
        ProjectStatus.ProjectStatusEnum status
    )
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(name)) errors.Add(DomainErrors.ProjectErrors.NAME_NOT_EMPTY);
        if (name.Length > Rules.NAME_MAX_LENGTH) errors.Add(DomainErrors.ProjectErrors.NAME_TOO_LONG);

        DomainResult<ProjectStatus> projectStatusResult = ProjectStatus.Create(status); 
        if(!projectStatusResult.IsSuccess) errors.AddRange(projectStatusResult.Errors);

        DomainResult<DateRange> dateRangeResult = DateRange.Create(startDate, endDate);
        if (!dateRangeResult.IsSuccess) errors.AddRange(dateRangeResult.Errors);

        if (errors.Count > 0)
        {
            return DomainResult<Project>.Failure()
                                        .WithErrors(errors);
        }

        Project project = new()
        {
            Id = ProjectId.NewId(),
            Name = name,
            ProjectPeriod = dateRangeResult.Value,
            Status = projectStatusResult.Value
        };
        return DomainResult<Project>.Success()
                                    .WithValue(project);
    }

    public DomainResult<Project> AddAssignment(
        string title,
        decimal estimatedHours,
        AssignmentStatus.AssignmentStatusEnum status,
        string? description,
        Guid? userId
    )
    {
        List<string> errors = [];   

        if (Status.IsCompleted())
        {
            return DomainResult<Project>.Failure()
                                        .WithErrors([DomainErrors.ProjectErrors.INVALID_ASSIGNMENT_PROJECT_COMPLETED]);
        }

        DomainResult<Assignment> assignmentResult = Assignment.Create(
            title,
            estimatedHours,
            status,
            description,
            userId
        );
        if (!assignmentResult.IsSuccess) errors.AddRange(assignmentResult.Errors); 
        
        if (errors.Count > 0)
        {
            return DomainResult<Project>.Failure()
                                        .WithErrors(errors);
        }

        return DomainResult<Project>.Success()
                                    .WithValue(this);
    }
}