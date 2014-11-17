
namespace TagKid.Core.Exceptions
{
    public static class E
    {
        public static void T(int errorCode, string message = "", params object[] args)
        {
            throw new TagKidException(errorCode, message, args);
        }
    }
}
