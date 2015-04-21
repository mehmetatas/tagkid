using System.Web;

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