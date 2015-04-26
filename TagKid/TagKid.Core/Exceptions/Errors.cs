using TagKid.Framework.Exceptions;

namespace TagKid.Core.Exceptions
{
    public static class Errors
    {
        public static readonly Error S_LoginRequired = new Error(100, "S_LoginRequired");
        public static readonly Error S_LoginTokenExpired = new Error(101, "S_LoginTokenExpired");
    }
}
