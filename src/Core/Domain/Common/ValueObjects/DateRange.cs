using Core.Contracts.Models;
using Core.Domain.Common.DomainResults;

namespace Core.Domain.Common.ValueObjects;
public sealed record DateRange
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    private DateRange() { }
    private DateRange(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static DomainResult<DateRange> Create
    (
        DateTime startDate,
        DateTime endDate
    )
    {
        OperationResult validationResult = Validate(startDate, endDate);
        if (!validationResult.IsSuccess)
        {
            return DomainResult<DateRange>.Failure([..validationResult.Errors]);
        }

        DateRange dateRange = new(startDate, endDate);
        return DomainResult<DateRange>.Success(dateRange)
                                      .WithDescription("DateRange created.");
    }

    public DomainResult<DateRange> Update(DateTime? newStartDate, DateTime? newEndDate)
    {
        DateTime updatedStartDate = newStartDate ?? StartDate;
        DateTime updatedEndDate = newEndDate ?? EndDate;

        OperationResult validationResult = Validate(updatedStartDate, updatedEndDate);
        if (!validationResult.IsSuccess)
        {
            return DomainResult<DateRange>.Failure([..validationResult.Errors]);
        }

        int updatedCount = 0;
        if (!StartDate.Equals(updatedStartDate)) updatedCount++;
        if (!EndDate.Equals(updatedEndDate)) updatedCount++;

        return DomainResult<DateRange>.Success(this)
                                      .WithUpdatedFieldCount(updatedCount);

    }

    private static OperationResult Validate(DateTime startDate, DateTime endDate)
    {
        List<string> errors = [];

        if (startDate >= endDate) errors.Add(DomainErrors.START_DATE_GREATER_THAN_END_DATE);

        if(errors.Count > 0)
        {
            return OperationResult.Failure(errors);
        }

        return OperationResult.Success();
    }
}