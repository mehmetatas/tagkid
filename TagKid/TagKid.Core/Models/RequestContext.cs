using Taga.Core.Context;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Models
{
    public class RequestContext
    {
        private RequestContext()
        {
        }

        public User User { get; set; }

        public static RequestContext Current
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