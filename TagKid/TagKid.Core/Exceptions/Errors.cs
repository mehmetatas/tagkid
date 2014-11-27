
namespace TagKid.Core.Exceptions
{
    public static class Errors
    {
        public static readonly Error Unknown = new Error(-1, "Error_Unknown");

        #region Validation

        public static readonly Error Validation_GenericError = new Error(1, "Validation_GenericError");

        public static readonly Error Validation_SignUp_Password = new Error(2, "Validation_SignUp_Password");
        public static readonly Error Validation_SignUp_Username = new Error(3, "Validation_SignUp_Username");
        public static readonly Error Validation_SignUp_EmailAddress = new Error(4, "Validation_SignUp_EmailAddress");
        public static readonly Error Validation_SignUp_Fullname = new Error(5, "Validation_SignUp_Fullname");
        public static readonly Error Validation_SignUp_EmailAlreadyExists = new Error(6, "Validation_SignUp_EmailAlreadyExists");
        public static readonly Error Validation_SignUp_UsernameAlreadyExists = new Error(7, "Validation_SignUp_UsernameAlreadyExists");

        public static readonly Error Validation_SignIn_EmptyPassword = new Error(8, "Validation_SignIn_EmptyPassword");
        public static readonly Error Validation_SignIn_EmptyUsernameOrEmail = new Error(9, "Validation_SignIn_EmptyUsernameOrEmail");

        #endregion

        #region Security

        public static readonly Error Security_InvalidAuthToken = new Error(100, "Security_InvalidAuthToken");
        public static readonly Error Security_UserInactive = new Error(101, "Security_UserInactive");
        public static readonly Error Security_InvalidUsernameOrPassword = new Error(102, "Security_InvalidUsernameOrPassword");
        public static readonly Error Security_ActivateAccount_InvalidActivationCode = new Error(103, "Security_ActivateAccount_InvalidActivationCodes");
        
        #endregion
    }
}