namespace Core.Utilities.Validations;
public static class ValidationMessages
{
    public static string NOT_EMPTY = "The {PropertyName} field cannot be empty.";
    public static string MAX_LENGTH = "The {PropertyName} field cannot exceed {MaxLength} characters.";
    public static string MIN_LENGTH = "The {PropertyName} field cannot be less than {MinLength} characters.";
    public static string GREATER_THAN_ZERO = "The {PropertyName} field must be greater than 0.";
    public static string INVALID_EMAIL = "The {PropertyName} field must be a valid email address.";
}