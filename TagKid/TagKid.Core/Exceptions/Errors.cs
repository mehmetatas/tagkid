
namespace TagKid.Core.Exceptions
{
    public static class Errors
    {
        public static readonly Error Unknown = new Error(-1, "Error_Unknown", ErrorType.Unknown);

        #region Validation

        public static readonly Error V_GenericError = new Error(1, "V_GenericError", ErrorType.Validation);

        public static readonly Error V_Password = new Error(2, "V_Password", ErrorType.Validation);
        public static readonly Error V_Username = new Error(3, "V_Username", ErrorType.Validation);
        public static readonly Error V_EmailAddress = new Error(4, "V_EmailAddress", ErrorType.Validation);
        public static readonly Error V_Fullname = new Error(5, "V_Fullname", ErrorType.Validation);
        public static readonly Error V_EmailAlreadyExists = new Error(6, "V_EmailAlreadyExists", ErrorType.Validation);
        public static readonly Error V_UsernameAlreadyExists = new Error(7, "V_UsernameAlreadyExists", ErrorType.Validation);
        public static readonly Error V_UsernameOrEmail = new Error(8, "V_UsernameOrEmail", ErrorType.Validation);

        #endregion

        #region Security

        public static readonly Error S_InvalidAuthToken = new Error(100, "S_InvalidAuthToken", ErrorType.Security);
        public static readonly Error S_UserInactive = new Error(101, "S_UserInactive", ErrorType.Security);
        public static readonly Error S_InvalidUsernameOrPassword = new Error(102, "S_InvalidUsernameOrPassword", ErrorType.Security);
        public static readonly Error S_InvalidActivationCode = new Error(103, "S_InvalidActivationCode", ErrorType.Security);
        
        #endregion
    }
}