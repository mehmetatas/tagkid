
namespace TagKid.Core.Exceptions
{
    public static class Throw
    {
        public static void User(Error error, string message = null, params object[] args)
        {
            throw new UserException(error, message, args);
        }

        public static void Critical(Error error, string message = null, params object[] args)
        {
            throw new CriticalException(error, message, args);
        }
    }
}
