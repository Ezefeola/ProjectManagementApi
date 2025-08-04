namespace Core.Contracts.DTOs.Assignment.Request;
public sealed record UpdateAssignmentDetailsRequestDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? EstimatedHours { get; set; }
}