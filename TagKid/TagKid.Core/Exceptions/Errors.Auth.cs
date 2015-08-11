using TagKid.Framework.Exceptions;

namespace TagKid.Core.Exceptions
{
    public static partial class Errors
    {
        public static readonly Error Auth_LoginRequired = new Error(100, "Auth_LoginRequired");
        public static readonly Error Auth_LoginTokenExpired = new Error(101, "Auth_LoginTokenExpired");
        public static readonly Error Auth_UsernameAlreadyExists = new Error(102, "Auth_UsernameAlreadyExists");
        public static readonly Error Auth_EmailAlreadyExists = new Error(103, "Auth_EmailAlreadyExists");
        public static readonly Error Auth_UserAwaitingActivation = new Error(104, "Auth_UserAwaitingActivation");
        public static readonly Error Auth_UserInactive = new Error(105, "Auth_UserInactive");
        public static readonly Error Auth_UserBanned = new Error(106, "Auth_UserBanned");
    }
}
