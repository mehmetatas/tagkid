using System.Web;
using TagKid.Framework.Context.Impl;

namespace TagKid.Framework.Context
{
    public static class CallContext
    {
        public static ICallContext Current
        {
            get
            {
                return HttpContext.Current == null
                    ? RuntimeContext.Instance
                    : WebCallContext.Instance;
            }
        }
    }
}