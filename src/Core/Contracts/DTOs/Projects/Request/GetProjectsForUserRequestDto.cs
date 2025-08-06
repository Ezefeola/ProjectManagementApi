using Core.Utilities.QueryOptions.Pagination;

namespace Core.Contracts.DTOs.Projects.Request;
public sealed record GetProjectsForUserRequestDto : IHasPaginationOptions
{
    public int? PageIndex { get; set; } 

    public int? PageSize { get; set; }
}