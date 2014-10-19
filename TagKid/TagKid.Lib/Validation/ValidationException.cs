using System;
using TagKid.Lib.Exceptions;

namespace TagKid.Lib.Validation
{
    public class ValidationException : UserException
    {
        public ValidationException(string message, params object[] args)
            : base(message, args)
        {
        }

        public ValidationException(Exception cause, string message, params object[] args)
            : base(cause, message, args)
        {
        }
    }
}