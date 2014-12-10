using System;
using TagKid.Core.Utils;

namespace TagKid.Core.Exceptions
{
    public class Error
    {
        private string _message;
        private object[] _messageArgs;

        internal Error(int code, string messageCode, ErrorType type)
        {
            Type = type;
            MessageCode = messageCode;
            Code = code;
        }

        public int Code { get; private set; }
        public string MessageCode { get; private set; }
        public ErrorType Type { get; private set; }

        public Error WithArgs(params object[] args)
        {
            return new Error(Code, MessageCode, Type)
            {
                _messageArgs = args
            };
        }

        public TagKidException ToException(string message = null, params object[] args)
        {
            return new TagKidException(this, message, args);
        }

        public string Message
        {
            get
            {
                return _message ?? (_message = String.Format(ML.GetValue(MessageCode), _messageArgs));
            }
        }
    }
}