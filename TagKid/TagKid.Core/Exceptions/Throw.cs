
namespace TagKid.Core.Exceptions
{
    public static class Throw
    {
        public static void Error(Error error, string message = null, params object[] args)
        {
            throw new TagKidException(error, message, args);
        }
    }
}
