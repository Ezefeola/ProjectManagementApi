using Core.Contracts.Models;
using Core.Domain.Abstractions;

namespace Core.Domain.Abstractions.ValueObjects;
public sealed record FullName : ValueObject
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;

    private FullName() { }
    private FullName(
        string firstName,
        string lastName
    )
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static DomainResult<FullName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return DomainResult<FullName>.Failure()
                                         .WithErrors([DomainErrors.UserErrors.FIRSTNAME_NOT_EMPTY]);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return DomainResult<FullName>.Failure()
                                         .WithErrors([DomainErrors.UserErrors.LASTNAME_NOT_EMPTY]);
        }

        FullName fullName = new(firstName, lastName);

        return DomainResult<FullName>.Success()
                                     .WithDescription("FullName created successfully.")
                                     .WithValue(fullName);
    }

    public DomainResult<FullName> UpdateIfChanged(
        string? newFirstName,
        string? newLastName
    )
    {
        string? updatedFirstName = newFirstName ?? FirstName;
        string? updatedLastName = newLastName ?? LastName;

        if (updatedFirstName == FirstName && updatedLastName == LastName)
        {
            return DomainResult<FullName>.Success()
                                         .WithDescription("FullName remains unchanged.")
                                         .WithValue(this)
                                         .WithUpdatedFieldCount(0);
        }

        DomainResult<FullName> fullNameResult = Create(updatedFirstName, updatedLastName);
        if (!fullNameResult.IsSuccess)
        {
            return fullNameResult;
        }

        int updatedCount = 0;
        if (updatedFirstName != FirstName) updatedCount++;
        if (updatedLastName != LastName) updatedCount++;


        return fullNameResult.WithUpdatedFieldCount(updatedCount);
    }
}