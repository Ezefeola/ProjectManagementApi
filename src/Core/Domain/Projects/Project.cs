using Core.Domain.Abstractions;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Common.ValueObjects;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects;
public sealed class Project : AggregateRoot<ProjectId>
{
    public static class Rules 
    {
        public const int NAME_MAX_LENGTH = 255;
        public const int STATUS_MAX_LENGTH = 255;
    }

    private Project() { }

    public string Name { get; private set; } = default!;
    public DateRange ProjectPeriod { get; private set; } = default!;
    public ProjectStatus Status { get; private set; } = default!;
    private readonly List<Assignment> _assignments = [];
    public IReadOnlyList<Assignment> Assignments => _assignments;
    private readonly List<User> _users = [];
    public IReadOnlyList<User> Users => _users;
    private readonly List<ProjectUser> _projectUsers = [];
    public IReadOnlyList<ProjectUser> ProjectUsers => _projectUsers;

    public static DomainResult<Project> Create(
        string name,
        DateTime startDate,
        DateTime endDate,
        ProjectStatus.ProjectStatusEnum status
    )
    {
        List<string> errors = [];

        DomainResult validationResult = Validate(name);
        if(!validationResult.IsSuccess) errors.AddRange(validationResult.Errors);

        DomainResult<ProjectStatus> projectStatusResult = ProjectStatus.Create(status); 
        if(!projectStatusResult.IsSuccess) errors.AddRange(projectStatusResult.Errors);

        DomainResult<DateRange> dateRangeResult = DateRange.Create(startDate, endDate);
        if (!dateRangeResult.IsSuccess) errors.AddRange(dateRangeResult.Errors);

        if (errors.Count > 0)
        {
            return DomainResult<Project>.Failure(errors);
        }

        Project project = new()
        {
            Id = ProjectId.NewId(),
            Name = name,
            ProjectPeriod = dateRangeResult.Value,
            Status = projectStatusResult.Value
        };
        return DomainResult<Project>.Success(project);
    }

    public DomainResult AddAssignment(
        string title,
        decimal? estimatedHours,
        AssignmentStatus.AssignmentStatusEnum status,
        string? description,
        UserId? userId
    )
    {
        if (Status.IsCompleted())
        {
            return DomainResult.Failure([DomainErrors.ProjectErrors.INVALID_ASSIGNMENT_PROJECT_COMPLETED]);
        }

        List<string> errors = [];   

        DomainResult<Assignment> assignmentResult = Assignment.Create(
            Id,
            title,
            estimatedHours,
            status,
            description,
            userId
        );
        if (!assignmentResult.IsSuccess) errors.AddRange(assignmentResult.Errors); 
        
        if (errors.Count > 0)
        {
            return DomainResult.Failure(errors);
        }

        _assignments.Add(assignmentResult.Value);

        return DomainResult.Success();
    }

    private static DomainResult Validate(string name)
    {
        List<string> errors = [];
        
        if (string.IsNullOrWhiteSpace(name)) errors.Add(DomainErrors.ProjectErrors.NAME_NOT_EMPTY);
        if (name.Length > Rules.NAME_MAX_LENGTH) errors.Add(DomainErrors.ProjectErrors.NAME_TOO_LONG);

        if (errors.Count > 0)
        {
            return DomainResult.Failure(errors);
        }

        return DomainResult.Success();
    }
}