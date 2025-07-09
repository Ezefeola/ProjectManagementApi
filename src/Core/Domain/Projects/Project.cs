using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Projects.ValueObjects;

namespace Core.Domain.Projects;
public sealed class Project : Entity<ProjectId>
{
    private Project() { }

    public string Name { get; set; } = default!;
    public DateRange ProjectPeriod { get; set; } = default!;
    public ProjectStatus Status { get; set; } = default!;
    public int Tasks { get; set; }
    public int TeamMembers { get; set; }
}