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
        if (string.IsNullOrWhiteSpace(value) || !IsValidPhoneNumber(value))
        {
            return DomainResult<PhoneNumber>.Failure()
                                            .WithErrors(["Invalid phone number."]);
        }

        PhoneNumber phoneNumber = new PhoneNumber(value);
        return DomainResult<PhoneNumber>.Success()
                                        .WithValue(phoneNumber);
    }

    private static bool IsValidPhoneNumber(string value)
    {
        return Regex.IsMatch(value, @"^\+?\d{7,15}$");
    }
}