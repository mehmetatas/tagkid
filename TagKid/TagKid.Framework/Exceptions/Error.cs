using System;
using TagKid.Framework.Utils;

namespace TagKid.Framework.Exceptions
{
    public class Error
    {
        private string _message;
        private object[] _messageArgs;

        protected internal Error(int code, string messageCode)
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

        public Error WithArgs(params object[] args)
        {
            return new Error(Code, MessageCode)
            {
                _messageArgs = args
            };
        }

        public TagKidException ToException()
        {
            return new TagKidException(this);
        }
    }
}
