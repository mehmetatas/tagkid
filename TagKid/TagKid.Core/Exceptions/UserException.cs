using System;

namespace TagKid.Core.Exceptions
{
    public class UserException : TagKidException
    {
        public UserException(string message, params object[] args)
            : base(message, args)
        {
        }

        public UserException(Exception cause, string message, params object[] args)
            : base(cause, message, args)
        {
        }
    }
}