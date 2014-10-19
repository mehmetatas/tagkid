using System;

namespace TagKid.Lib.Exceptions
{
    public class SystemException : TagKidException
    {
        public SystemException(string message, params object[] args)
            : base(message, args)
        {
        }

        public SystemException(Exception cause, string message, params object[] args)
            : base(cause, message, args)
        {
        }
    }
}