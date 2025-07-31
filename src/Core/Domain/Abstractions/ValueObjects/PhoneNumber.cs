using Core.Contracts.Models;
using System.Text.RegularExpressions;

namespace Core.Domain.Abstractions.ValueObjects;
public sealed record PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static DomainResult<PhoneNumber> Create(string value)
    {
        OperationResult validationResult = Validate(value);
        if(!validationResult.IsSuccess)
        {
            return DomainResult<PhoneNumber>.Failure([..validationResult.Errors]);
        }

        PhoneNumber phoneNumber = new(value);
        return DomainResult<PhoneNumber>.Success(phoneNumber);
    }

    private static OperationResult Validate(string value)
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(value)) errors.Add("Phone number cannot be empty.");
        if (!IsValidPhoneNumber(value)) errors.Add("Phone number format is invalid.");

        return OperationResult.Success();
    }

    private static bool IsValidPhoneNumber(string value)
    {
        return Regex.IsMatch(value, @"^\+?\d{7,15}$");
    }
}