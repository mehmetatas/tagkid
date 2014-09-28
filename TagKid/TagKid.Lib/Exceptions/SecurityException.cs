using System;

namespace TagKid.Lib.Exceptions
{
    public class SecurityException: TagKidException
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
