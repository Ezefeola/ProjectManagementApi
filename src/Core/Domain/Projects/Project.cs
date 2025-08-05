using Core.Domain.Abstractions;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Common.ValueObjects;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects;
public sealed class Project : AggregateRoot<ProjectId>
{
    public static class Rules 
    {
        public const int NAME_MAX_LENGTH = 255;
        public const int DESCRIPTION_MAX_LENGTH = 1000;
        public const int STATUS_MAX_LENGTH = 255;
    }

    private Project() { }

    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateRange ProjectPeriod { get; private set; } = default!;
    public ProjectStatus Status { get; private set; } = default!;
    private readonly List<Assignment> _assignments = [];
    public IReadOnlyList<Assignment> Assignments => _assignments;
    private readonly List<ProjectUser> _projectUsers = [];
    public IReadOnlyList<ProjectUser> ProjectUsers => _projectUsers;

    public static DomainResult<Project> Create(
        string name,
        string? description,
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
            Description = description,
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

        DomainResult<Assignment> assignmentResult = Assignment.Create(
            Id,
            title,
            estimatedHours,
            status,
            description,
            userId
        );
        if (!assignmentResult.IsSuccess) return DomainResult.Failure(assignmentResult.Errors);

        _assignments.Add(assignmentResult.Value);

        return DomainResult.Success();
    }

    public DomainResult AssignUser(
        UserId userId, 
        ProjectUser.ProjectUserRoleEnum role, 
        bool isUserAssigned
    )
    {
        if(isUserAssigned)
        {
            return DomainResult.Failure([DomainErrors.ProjectErrors.USER_ALREADY_ASSIGNED]);
        }

        ProjectUser projectUser = ProjectUser.Create(Id, userId, role);
        _projectUsers.Add(projectUser);
        MarkAsUpdated();

        return DomainResult.Success();
    }

    public DomainResult UpdateAssignmentDetails(
        AssignmentId assignmentId,
        string? title, 
        string? description, 
        decimal? estimatedHours
    )
    {
        Assignment? assignment = _assignments.FirstOrDefault(x => x.Id == assignmentId);
        if(assignment is null)
        {
            return DomainResult.Failure([DomainErrors.AssignmentErrors.ASSIGNMENT_NOT_FOUND]);
        }

        DomainResult updateAssignmentDetailsResult = assignment.UpdateDetails(title, description, estimatedHours);
        if(!updateAssignmentDetailsResult.IsSuccess) 
        {
            return DomainResult.Failure(updateAssignmentDetailsResult.Errors); 
        }
        if(updateAssignmentDetailsResult.IsUpdated)
        {
            MarkAsUpdated();
        }

        return DomainResult.Success();
    }

    public DomainResult ChangeAssignmentStatus(
        AssignmentId assignmentId,
        AssignmentStatus.AssignmentStatusEnum status
    )
    {
        Assignment? assignment = _assignments.FirstOrDefault(x => x.Id == assignmentId);
        if(assignment is null)
        {
            return DomainResult.Failure([DomainErrors.AssignmentErrors.ASSIGNMENT_NOT_FOUND]);
        }

        DomainResult changeAssignmentStatusResult =  assignment.ChangeStatus(status);
        if(!changeAssignmentStatusResult.IsSuccess)
        {
            return DomainResult.Failure(changeAssignmentStatusResult.Errors);
        }

        if(changeAssignmentStatusResult.IsUpdated)
        {
            MarkAsUpdated();
        }

        return DomainResult.Success();
    }

    public void RemoveAssignment(AssignmentId assignmentId)
    {
        Assignment? assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId);
        if(assignment is null)
        {
            return;
        }

        assignment.SoftDelete();
    }

    public DomainResult RemoveUser(UserId userId)
    {
        ProjectUser? projectUser = _projectUsers.FirstOrDefault(x => x.UserId == userId);
        if (projectUser is null)
        {
            return DomainResult.Failure([DomainErrors.ProjectErrors.USER_NOT_ASSIGNED]);
        }

        projectUser.SoftDelete();

        return DomainResult.Success();
    }

    public DomainResult UpdateDetails(
        string? name, 
        string? description, 
        DateTime? startDate, 
        DateTime? endDate
    )
    {
        List<string> errors = [];

        if (!string.IsNullOrWhiteSpace(name))
        {
            DomainResult changeNameResult = ChangeName(name);
            if(!changeNameResult.IsSuccess) errors.AddRange(changeNameResult.Errors);
        }
        if(!string.IsNullOrWhiteSpace(description))
        {
            DomainResult changeDescriptionResult = ChangeDescription(description);
            if(!changeDescriptionResult.IsSuccess) errors.AddRange(changeDescriptionResult.Errors);
        }
        if(startDate.HasValue || endDate.HasValue)
        {
            DomainResult changeProjectPeriodResult = ChangeProjectPeriod(startDate, endDate);
            if(!changeProjectPeriodResult.IsSuccess) errors.AddRange(changeProjectPeriodResult.Errors);
        }

        if(errors.Count > 0)
        {
            return DomainResult.Failure(errors);
        }

        return DomainResult.Success();
    }
    public DomainResult ChangeName(string? name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            return DomainResult.Success()
                               .WithDescription("No changes made to project name.");
        }

        if(name.Length > Rules.NAME_MAX_LENGTH)
        {
            return DomainResult.Failure([DomainErrors.ProjectErrors.NAME_TOO_LONG]);
        }   
        
        if(Name != name)
        {
            Name = name;
            MarkAsUpdated();
        }
        return DomainResult.Success()
                           .WithDescription("Project name changed successfully.");
    }
    public DomainResult ChangeDescription(string? description)
    {
        if(Description != description)
        {
            Description = description;
            MarkAsUpdated();   
        }
        return DomainResult.Success()
                           .WithDescription("Project description changed successfully.");
    }
    public DomainResult ChangeProjectPeriod(DateTime? startDate, DateTime? endDate)
    {
        DomainResult updateProjectPeriodResult = ProjectPeriod.Update(startDate, endDate);
        if (!updateProjectPeriodResult.IsSuccess)
        {
            return DomainResult.Failure(updateProjectPeriodResult.Errors);
        }

        MarkAsUpdated();
        return DomainResult.Success()
                           .WithDescription("Project period changed successfully.");
    }

    public override void SoftDelete()
    {
        base.SoftDelete();

        foreach (Assignment assignment in _assignments)
        {
            assignment.SoftDelete();
        }

        foreach (ProjectUser projectUser in _projectUsers)
        {
            projectUser.SoftDelete();
        }
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