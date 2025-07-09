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
        if (startDate >= endDate)
        {
            return DomainResult<DateRange>.Failure()
                                          .WithErrors([DomainErrors.START_DATE_BEFORE_END_DATE]);
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

        DomainResult<DateRange> createResult = Create(updatedStartDate, updatedEndDate);
        if (!createResult.IsSuccess)
            return createResult;

        int updatedCount = 0;
        if (!StartDate.Equals(updatedStartDate)) updatedCount++;
        if (!EndDate.Equals(updatedEndDate)) updatedCount++;

        return createResult.WithUpdatedFieldCount(updatedCount);

    }
}