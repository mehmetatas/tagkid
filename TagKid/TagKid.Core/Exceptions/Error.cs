using System;
using System.Collections.Generic;
using TagKid.Core.Models;
using TagKid.Core.Utils;

namespace TagKid.Core.Exceptions
{
    public class Error
    {
        private readonly IDictionary<string, string> _messages;

        internal Error(int code, IDictionary<string, string> messages)
        {
            Code = code;
            _messages = new SortedDictionary<string, string>(messages);
        }

        public int Code { get; private set; }

        public string Message
        {
            get
            {
                var culture = RequestContext.Current.Culture;

                if (String.IsNullOrEmpty(culture) || !_messages.ContainsKey(culture))
                {
                    culture = Cultures.EnGb;
                }

                return _messages.ContainsKey(culture) ? _messages[culture] : String.Empty;
            }
        }
    }
}