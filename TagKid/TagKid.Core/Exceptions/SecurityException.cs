using System;

namespace TagKid.Core.Exceptions
{
    public class SecurityException : TagKidException
    {
        public SecurityException(string message, params object[] args)
            : base(message, args)
        {
        }

        public SecurityException(Exception cause, string message, params object[] args)
            : base(cause, message, args)
        {
        }
    }
}