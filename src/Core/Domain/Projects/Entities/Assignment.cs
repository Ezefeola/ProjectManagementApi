using Core.Domain.Abstractions;
using Core.Domain.Collaborators;
using Core.Domain.Projects.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class Assignment : Entity<AssignmentId>
{
    private Assignment() { }

    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal EstimatedHours { get; private set; } = default!;
    public decimal LoggedHours { get; private set; } = default!;
    public AssignmentStatus Status { get; private set; } = default!;
    public List<Collaborator> Collaborators { get; set; } = default!;


}
