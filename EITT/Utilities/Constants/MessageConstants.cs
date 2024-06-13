/*
 * Created by   : Jahangir
 * Date created : 16.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  : 
 * Reviewed Date:
 */
namespace Utilities.Constants
{
    public static class MessageConstants
    {
        //Common
        public const string RequiredFieldError = "Required";

        public const string DuplicateError = "Duplicate data found";

        public const string GenericError = "Something went wrong! Please try after sometime. If you are experiencing similar problem frequently, please report it to helpdesk.";

        public const string InvalidParameterError = "Invalid parameter(s)!";

        public const string NoMatchFoundError = "No match found!";

        public const string UnauthorizedAttemptOfRecordUpdateError = "Unauthorized attempt of updating record!";

        public const string IfFutureDate = "This date should not be a future date!";


        //USER PROFILE REGISTRATION:
        public const string UsernameTaken = "Username already exists!";

        public const string PasswordLengthError = "Password should be at least 8 characters!";

        public const string PasswordMatchError = "The Passwords do not match!";

        public const string UpdateMessage = "Successfully Updated";

        public const string WrongPasswordError = "Wrong password!";

        //User Login
        public const string InvalidLogin = "Invalid username or password!";

        public const string RecoveryRequestExists = "You cannot Login until your Recovery Request Processed by Admin";

        public const string SomethingWentWrong = "Something went wrong! Please review the form below.";

    }
}