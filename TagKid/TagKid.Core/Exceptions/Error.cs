using TagKid.Core.Utils;

namespace TagKid.Core.Exceptions
{
    public class Error
    {
        private string _message;

        internal Error(int code, string messageCode)
        {
            MessageCode = messageCode;
            Code = code;
        }

        public int Code { get; private set; }
        public string MessageCode { get; private set; }

        public string Message
        {
            get
            {
                return _message ?? (_message = ML.GetValue(MessageCode));
            }
        }
    }
}