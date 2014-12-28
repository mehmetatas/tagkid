using Taga.Core.Context;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Models
{
    public class RequestContext
    {
        private readonly string _culture;
        private Token _authToken;

        private RequestContext()
        {
            _culture = "en-GB";
        }

        public static bool IsAuthenticated
        {
            get { return Current._authToken != null && Current._authToken.User != null; }
        }

        public static Token AuthToken 
        {
            get { return Current._authToken; }
            set { Current._authToken = value; }
        }

        public static User User
        {
            get { return Current._authToken.User; }
        }

        private static RequestContext Current
        {
            get
            {
                var context = CallContext.Current["TagKid.RequestContext"] as RequestContext;
                if (context == null)
                {
                    context = new RequestContext();
                    CallContext.Current["TagKid.RequestContext"] = context;
                }
                return context;
            }
        }
    }
}