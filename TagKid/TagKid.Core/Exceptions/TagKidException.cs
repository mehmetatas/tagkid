using Taga.Core.Exceptions;

namespace TagKid.Core.Exceptions
{
    public abstract  class TagKidException : TagaAppException
    {
        protected TagKidException(Error error, string message = null, params object[] args)
            : base(message ?? error.Message, args)
        {
            Error = error;
        }

        public Error Error { get; private set; }
    }

    public static class TagKidExceptionExtensions
    {
        public static bool IsCritical(this TagKidException exception)
        {
            return exception is CriticalException;
        }
    }
}