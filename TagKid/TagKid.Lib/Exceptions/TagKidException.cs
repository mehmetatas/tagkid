using System;

namespace TagKid.Lib.Exceptions
{
    public abstract class TagKidException : Exception
    {
        protected TagKidException(string message, params object[] args)
            : this(null, message, args)
        {
        }

        protected TagKidException(Exception cause, string message, params object[] args)
            : base(String.Format(message, args), cause)
        {
        }
    }
}
