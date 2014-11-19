
namespace TagKid.Core.Exceptions
{
    public static class E
    {
        public static void x(Error error, string message = null, params object[] args)
        {
            throw new TagKidException(error, message, args);
        }
    }
}
