using Core.Contracts.Models;
using Core.Contracts.Results;
using Core.Domain.Abstractions;
using Core.Domain.Common.DomainResults;
using System.Text.RegularExpressions;

namespace Core.Domain.Common.ValueObjects;
public sealed record EmailAddress : ValueObject
{
    public string Value { get; private set; } = default!;

    private EmailAddress() { }
    private EmailAddress(string value) => Value = value;

    public static DomainResult<EmailAddress> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DomainResult<EmailAddress>.Failure([DomainErrors.UserErrors.EMAIL_NOT_EMPTY]);
        }

        if (!IsValidEmail(value))
        {
            return DomainResult<EmailAddress>.Failure([DomainErrors.UserErrors.EMAIL_INVALID_FORMAT]);
        }

        EmailAddress emailAddress = new(value);
        return DomainResult<EmailAddress>.Success(emailAddress)
                                         .WithDescription("EmailAddress created successfully.");
    }
    public DomainResult UpdateIfChanged(string? email)
    {
        if(email is null)
        {
            return DomainResult.Success();
        }
        if (email == Value)
        {
            return DomainResult.Success()
                               .WithDescription("EmailAddress remains unchanged.");
        }
       
        DomainResult validationResult = Validate(email);
        if (!validationResult.IsSuccess)
        {
            return DomainResult.Failure([..validationResult.Errors]);
        }

        Value = email;

        return DomainResult.Success();
    }

    private static DomainResult Validate(string email)
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(email)) errors.Add(DomainErrors.UserErrors.EMAIL_NOT_EMPTY);

        if (!IsValidEmail(email)) errors.Add(DomainErrors.UserErrors.EMAIL_INVALID_FORMAT);

        if(errors.Count > 0)
        {
            return DomainResult.Failure(errors);
        }

        return DomainResult.Success();
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