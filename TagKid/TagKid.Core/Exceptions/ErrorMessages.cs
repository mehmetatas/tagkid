using System;
using System.Collections;

namespace TagKid.Core.Exceptions
{
    public static class ErrorMessages
    {
        public static string Get(int errorCode)
        {
            return String.Format("{0} [{1}]", ErrorMessagesEnGb[errorCode], errorCode);
        }

        private readonly static Hashtable ErrorMessagesEnGb = new Hashtable();

        static ErrorMessages()
        {
            ErrorMessagesEnGb.Add(ErrorCodes.Unknown, "Unknown error has occured!");

            ErrorMessagesEnGb.Add(ErrorCodes.Security_InvalidAuthToken, "Invalid Auth Token!");
        }
    }
}
