﻿namespace Core.Domain.Abstractions;
public static class DomainErrors
{
    public static readonly string START_DATE_BEFORE_END_DATE = "Start date must be before end date.";
    public static class UserErrors
    {
        public static readonly string EMAIL_NOT_EMPTY = "The email field cannot be empty.";
        public static readonly string EMAIL_INVALID_FORMAT = "Invalid email format.";

        public static readonly string FIRSTNAME_NOT_EMPTY = "The FirstName field cannot be empty.";
        public static readonly string LASTNAME_NOT_EMPTY = "The LastNmae field cannot be empty.";

        public static readonly string ONE_TIME_PASSWORD_NOT_EMPTY = "OneTimePassword cannot be empty.";
    }

    public static class AssignmentErrors
    {
        public static readonly string INVALID_ASSIGNMENT_STATUS = "Invalid assignment status value.";
    }
}