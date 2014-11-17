using Taga.Core.Exceptions;

namespace TagKid.Core.Exceptions
{
    public class TagKidException : TagaAppException
    {
        public TagKidException(int errorCode, string message = "", params object[] args)
            : base(message, args)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; private set; }

        public string UserMessage
        {
            get { return ErrorMessages.Get(ErrorCode); }
        }
    }
}