using Core.Domain.Abstractions;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class ProjectUser : Entity
{
    public static class ColumnNames
    {
        public const string ProjectId = "ProjectId";
        public const string UserId = "UserId";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string Status = "Status";
    }
    public ProjectId ProjectId { get; private set; } = default!;
    public Project Project { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public User User { get; private set; } = default!;
}
