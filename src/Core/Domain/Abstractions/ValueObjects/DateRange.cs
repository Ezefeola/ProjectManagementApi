namespace Core.Domain.Abstractions.ValueObjects;
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
        List<string> errors = [];

        if (startDate >= endDate) errors.Add(DomainErrors.START_DATE_GREATER_THAN_END_DATE);

        if (errors.Count > 0)
        {
            return DomainResult<DateRange>.Failure()
                                          .WithErrors(errors);
        }

        DateRange dateRange = new(startDate, endDate);
        return DomainResult<DateRange>.Success()
                                      .WithDescription("DateRange created.")
                                      .WithValue(dateRange);
    }

    public DomainResult<DateRange> Update(DateTime? newStartDate, DateTime? newEndDate)
    {
        DateTime updatedStartDate = newStartDate ?? StartDate;
        DateTime updatedEndDate = newEndDate ?? EndDate;

        DomainResult<DateRange> createDateRangeResult = Create(updatedStartDate, updatedEndDate);
        if (!createDateRangeResult.IsSuccess)
        {
            return createDateRangeResult;
        }

        int updatedCount = 0;
        if (!StartDate.Equals(updatedStartDate)) updatedCount++;
        if (!EndDate.Equals(updatedEndDate)) updatedCount++;

        return createDateRangeResult.WithUpdatedFieldCount(updatedCount);

    }
}