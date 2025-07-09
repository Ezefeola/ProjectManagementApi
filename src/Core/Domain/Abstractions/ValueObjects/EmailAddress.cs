using Core.Contracts.Models;
using Core.Domain.Abstractions;
using System.Text.RegularExpressions;

namespace Core.Domain.Abstractions.ValueObjects;
public sealed record EmailAddress : ValueObject
{
    public string Value { get; } = default!;

    private EmailAddress() { }
    private EmailAddress(string value) => Value = value;

    public static DomainResult<EmailAddress> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DomainResult<EmailAddress>.Failure()
                                             .WithErrors([DomainErrors.UserErrors.EMAIL_NOT_EMPTY]);
        }

        if (!IsValidEmail(value))
        {
            return DomainResult<EmailAddress>.Failure()
                                             .WithErrors([DomainErrors.UserErrors.EMAIL_INVALID_FORMAT]);
        }

        EmailAddress emailAddress = new(value);

        return DomainResult<EmailAddress>.Success()
                                         .WithDescription("EmailAddress created successfully.")
                                         .WithValue(emailAddress);
    }
    public DomainResult<EmailAddress> UpdateIfChanged(string? newValue)
    {
        if (string.IsNullOrWhiteSpace(newValue))
        {
            return DomainResult<EmailAddress>.Success()
                                             .WithDescription("EmailAddress not updated because input is null or empty.")
                                             .WithValue(this);
        }

        if (newValue == Value)
        {
            return DomainResult<EmailAddress>.Success()
                                             .WithDescription("EmailAddress remains unchanged.")
                                             .WithValue(this);
        }

        DomainResult<EmailAddress> createResult = Create(newValue);
        if (!createResult.IsSuccess)
        {
            return createResult;
        }

        return createResult.WithUpdatedFieldCount(1);
    }
    private static bool IsValidEmail(string email)
    {
        string emailRegexFormat = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(
            email,
            emailRegexFormat,
            RegexOptions.IgnoreCase
        );
    }
}