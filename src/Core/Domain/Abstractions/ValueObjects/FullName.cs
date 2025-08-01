﻿using Core.Contracts.Models;

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
         OperationResult validationResult = Validate(firstName, lastName);
        if (!validationResult.IsSuccess)
        {
            return DomainResult<FullName>.Failure([..validationResult.Errors]);
        }

        FullName fullName = new(firstName, lastName);
        return DomainResult<FullName>.Success(fullName)
                                     .WithDescription("FullName created successfully.");
    }

    public DomainResult<FullName> UpdateIfChanged(
        string? firstName,
        string? lastName
    )
    {
        string? updatedFirstName = firstName ?? FirstName;
        string? updatedLastName = lastName ?? LastName;

        if (updatedFirstName == FirstName && updatedLastName == LastName)
        {
            return DomainResult<FullName>.Success(this)
                                         .WithDescription("FullName remains unchanged.")
                                         .WithUpdatedFieldCount(0);
        }

        OperationResult validationResult = Validate(updatedFirstName, updatedLastName);
        if (!validationResult.IsSuccess)
        {
            return DomainResult<FullName>.Failure([..validationResult.Errors]);
        }

        int updatedCount = 0;
        if (updatedFirstName != FirstName) updatedCount++;
        if (updatedLastName != LastName) updatedCount++;


        return DomainResult<FullName>.Success(this)
                                     .WithUpdatedFieldCount(updatedCount);
    }

    private static OperationResult Validate(string firstName, string lastName)
    {
        List<string> errors = [];
        if (string.IsNullOrWhiteSpace(firstName)) errors.Add(DomainErrors.UserErrors.FIRST_NAME_NOT_EMPTY);

        if (string.IsNullOrWhiteSpace(lastName)) errors.Add(DomainErrors.UserErrors.LAST_NAME_NOT_EMPTY);

        if (errors.Count > 0)
        {
            return OperationResult.Failure(errors);
        }

        return OperationResult.Success();
    }
}