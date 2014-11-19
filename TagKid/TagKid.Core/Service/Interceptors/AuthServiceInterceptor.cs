using System;
using System.Reflection;
using Taga.Core.DynamicProxy;

namespace TagKid.Core.Service.Interceptors
{
    public interface IAuthServiceInterceptor : IMethodCallInterceptor
    {
    }

    public class AuthServiceInterceptor : IAuthServiceInterceptor
    {
        public object BeforeCall(object obj, MethodInfo mi, object[] args)
        {
            return null;
        }

        public void AfterCall(object obj, MethodInfo mi, object[] args, object returnValue)
        {
        }

        public void OnError(object obj, MethodInfo mi, object[] args, Exception exception)
        {
        }
    }
}
