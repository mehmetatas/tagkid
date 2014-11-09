using System;
using TagKid.Core.Exceptions;

namespace TagKid.Core.Validation
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