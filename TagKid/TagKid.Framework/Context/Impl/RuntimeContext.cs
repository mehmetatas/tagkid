using RuntimeCallContext = System.Runtime.Remoting.Messaging.CallContext;

namespace TagKid.Framework.Context.Impl
{
    class RuntimeContext : ICallContext
    {
        internal static readonly ICallContext Instance = new RuntimeContext();

        private RuntimeContext()
        {
        }

        public object this[string key]
        {
            get { return RuntimeCallContext.GetData(key); }
            set { RuntimeCallContext.SetData(key, value); }
        }
    }
}
