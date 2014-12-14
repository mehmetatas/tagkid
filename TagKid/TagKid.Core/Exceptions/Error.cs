using System;
using TagKid.Core.Utils;

namespace TagKid.Core.Exceptions
{
    public class Error
    {
        private string _message;
        private object[] _messageArgs;

        internal Error(int code, string messageCode)
        {
            Code = code;
            MessageCode = messageCode;
        }

        public int Code { get; private set; }
        public string MessageCode { get; private set; }

        public string Message
        {
            get
            {
                return _message ?? (_message = String.Format(ML.GetValue(MessageCode), _messageArgs ?? new object[0]));
            }
        }

        public ErrorType Type 
        {
            get
            {
                if (Code >= 1 && Code <= 99)
                {
                    return ErrorType.Validation;
                }
                if (Code >= 100 && Code <= 199)
                {
                    return ErrorType.Security;
                }
                return ErrorType.Unknown;
            }
        }

        public Error WithArgs(params object[] args)
        {
            return new Error(Code, MessageCode)
            {
                _messageArgs = args
            };
        }

        public TagKidException ToException(string message = null, params object[] args)
        {
            return new TagKidException(this, message, args);
        }
    }
}