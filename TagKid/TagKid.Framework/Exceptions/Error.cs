using System;
using TagKid.Framework.Utils;

namespace TagKid.Framework.Exceptions
{
    public class Error : Exception
    {
        private readonly static object[] EmptyArgs = new object[0];

        private string _message;
        private object[] _messageArgs = EmptyArgs;

        public Error(int code, string messageCode)
        {
            Code = code;
            MessageCode = messageCode;
        }

        public int Code { get; private set; }
        public string MessageCode { get; private set; }
        
        public override string Message
        {
            get
            {
                return _message ?? (_message = String.Format(ML.GetValue(MessageCode), _messageArgs));
            }
        }

        public Error WithArgs(params object[] args)
        {
            return new Error(Code, MessageCode)
            {
                _messageArgs = args
            };
        }
    }
}
