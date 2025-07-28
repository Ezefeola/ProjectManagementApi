using Core.Domain.Abstractions;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;

namespace Core.Domain.Projects.Entities;
public sealed class ProjectUser : Entity<ProjectUserId>
{
    public static class ColumnNames
    {
        public const string ProjectId = "ProjectId";
        public const string CollaboratorId = "CollaboratorId";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string Status = "Status";
    }
    public Project Project { get; private set; } = default!;
    public User User { get; private set; } = default!;
}
