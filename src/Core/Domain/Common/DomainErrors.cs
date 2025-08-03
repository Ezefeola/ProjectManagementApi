using Core.Domain.Projects;
using Core.Domain.Projects.Entities;
using Core.Domain.Users;

namespace Core.Domain.Common;
public static class DomainErrors
{
    public static readonly string START_DATE_GREATER_THAN_END_DATE = "Start date must be before end date.";
    public static class UserErrors
    {
        public static readonly string EMAIL_NOT_EMPTY = "The email field cannot be empty.";
        public static readonly string EMAIL_INVALID_FORMAT = "Invalid email format.";

        public static readonly string FIRST_NAME_NOT_EMPTY = "The FirstName field cannot be empty.";
        public static readonly string LAST_NAME_NOT_EMPTY = "The LastName field cannot be empty.";

        public static readonly string PASSWORD_NOT_EMPTY = "Password cannot be empty.";
        public static readonly string PASSWORD_TOO_LONG = $"Password can not exceed {User.Rules.PASSWORD_MAX_LENGTH}.";

        public static readonly string USER_NOT_FOUND = $"User not found.";
        public static readonly string USER_ALREADY_EXISTS = "User already exists.";
    }

    public static class AssignmentErrors
    {
        public static readonly string INVALID_ASSIGNMENT_STATUS = "Invalid assignment status value.";
        public static readonly string TITLE_NOT_EMPTY = "The Title field cannot be empty.";
        public static readonly string TITLE_TOO_LONG = $"The Title field cannot exceed {Assignment.Rules.TITLE_MAX_LENGTH}.";
        public static readonly string DESCRIPTION_TOO_LONG = $"The Description field cannot exceed {Assignment.Rules.TITLE_MAX_LENGTH}.";
        public static readonly string ASSIGNMENT_NOT_FOUND = $"Assignment not found.";
    }

    public static class ProjectErrors
    {
        public static readonly string NAME_NOT_EMPTY = "The Name field cannot be empty.";
        public static readonly string NAME_TOO_LONG = $"The Name field cannot exceed {Project.Rules.NAME_MAX_LENGTH} characters.";
        public static readonly string START_DATE_BEFORE_END_DATE = "Start date must be before end date.";
        public static readonly string STATUS_NOT_EMPTY = "The Status field cannot be empty.";
        public static readonly string STATUS_TOO_LONG = $"The Status field cannot exceed {Project.Rules.STATUS_MAX_LENGTH} characters.";
        public static readonly string INVALID_STATUS = "Invalid status assignment, please assign a correct one.";
        public static readonly string INVALID_ASSIGNMENT_PROJECT_COMPLETED = "Cannot add an assignment to a completed project.";
        public static readonly string PROJECT_NOT_FOUND = "Project not found.";
    }   
}