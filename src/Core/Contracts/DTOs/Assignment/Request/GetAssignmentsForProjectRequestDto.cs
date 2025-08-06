using Core.Utilities.QueryOptions.Pagination;

namespace Core.Contracts.DTOs.Assignment.Request;
public sealed record GetAssignmentsForProjectRequestDto : IHasPaginationOptions
{
    public int? PageIndex { get; set; }

    public int? PageSize { get; set; }  
}