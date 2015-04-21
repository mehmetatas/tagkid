using System;

namespace TagKid.Framework.Exceptions
{
    public class TagKidException : Exception
    {
        public Error Error { get; private set; }

        public TagKidException(Error error)
        {
            Error = error;
        }
    }
}
