using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Collaborators;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;

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
    public List<Collaborator> Collaborators { get; private set; } = [];
    public List<ProjectUser> ProjectCollaborators { get; set; } = [];
}