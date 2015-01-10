
namespace TagKid.Core.Exceptions
{
    public static class Errors
    {
        public static readonly Error Unknown = new Error(-1, "Error_Unknown");

        #region Validation

        public static readonly Error V_GenericError = new Error(1, "V_GenericError");

        public static readonly Error V_Password = new Error(2, "V_Password");
        public static readonly Error V_Username = new Error(3, "V_Username");
        public static readonly Error V_EmailAddress = new Error(4, "V_EmailAddress");
        public static readonly Error V_Fullname = new Error(5, "V_Fullname");
        public static readonly Error V_EmailAlreadyExists = new Error(6, "V_EmailAlreadyExists");
        public static readonly Error V_UsernameAlreadyExists = new Error(7, "V_UsernameAlreadyExists");
        public static readonly Error V_UsernameOrEmail = new Error(8, "V_UsernameOrEmail");
        public static readonly Error V_TokenId = new Error(9, "V_TokenId");
        public static readonly Error V_Token = new Error(10, "V_Token");
        public static readonly Error V_ConfirmationCodeId = new Error(11, "V_ConfirmationCodeId");
        public static readonly Error V_ConfirmationCode = new Error(12, "V_ConfirmationCode");
        public static readonly Error V_SelectCategory = new Error(13, "V_SelectCategory");
        public static readonly Error V_TitleTooLong = new Error(14, "V_TitleTooLong");
        public static readonly Error V_ContentTooLong = new Error(15, "V_ContentTooLong");

        #endregion

        #region Security

        public static readonly Error S_InvalidAuthToken = new Error(100, "S_InvalidAuthToken");
        public static readonly Error S_UserInactive = new Error(101, "S_UserInactive");
        public static readonly Error S_InvalidUsernameOrPassword = new Error(102, "S_InvalidUsernameOrPassword");
        public static readonly Error S_InvalidActivationCode = new Error(103, "S_InvalidActivationCode");

        #endregion
    }
}