using Taga.Core.Exceptions;

namespace TagKid.Core.Exceptions
{
    public class TagKidException : TagaAppException
    {
        public TagKidException(Error error, string message = null, params object[] args)
            : base(message ?? error.Message, args)
        {
            Error = error;
        }

        public Error Error { get; private set; }
    }
}