
namespace TagKid.Core.Exceptions
{
    public static class ErrorCodes
    {
        public const int Unknown = -1;

        public const int Validation_PasswordTooShort = 1;
        public const int Validation_UsernameTooShort = 2;
        public const int Validation_InvalidEmailAddress = 3;

        public const int Security_InvalidAuthToken = 100;
        public const int Security_UserInactive = 101;
    }
}
