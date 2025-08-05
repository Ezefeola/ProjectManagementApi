using Core.Contracts.DTOs.Assignment.Response;
using Core.Domain.Projects.Entities;

namespace Core.Utilities.Mappers;
public static class AssignmentMappers
{
    public static AssignmentResponseDto ToAssignmentResponseDto(this Assignment assignment)
    {
        return new AssignmentResponseDto()
        {
            Id = assignment.Id.Value,
            Title = assignment.Title,
            Description = assignment.Description,
            EstimatedHours = assignment.EstimatedHours,
            Status = assignment.Status.Value,
            LoggedHours = assignment.LoggedHours,
            UserIds = [.. assignment.AssignmentUsers.Select(x => x.UserId.Value)]
        };
    }
}