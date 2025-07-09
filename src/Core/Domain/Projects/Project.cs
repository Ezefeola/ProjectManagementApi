using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Collaborators;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;

namespace Core.Domain.Projects;
public sealed class Project : AggregateRoot<ProjectId>
{
    private Project() { }

    public string Name { get; private set; } = default!;
    public DateRange ProjectPeriod { get; private set; } = default!;
    public ProjectStatus Status { get; private set; } = default!;
    public List<Assignment> Assignments { get; private set; }
    public List<Collaborator> Collaborators { get; private set; } = default!;
}