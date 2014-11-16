using System;
using System.Reflection;
using Taga.Core.Rest;

namespace TagKid.Core.Service
{
    public class TagKidActionInterceptor : IActionInterceptor
    {
        public void BeforeCall(MethodInfo actionMethod, object[] parameters)
        {
            
        }

        public void AfterCall(MethodInfo actionMethod, object[] parameters, object returnValue)
        {
            
        }

        public object OnException(MethodInfo actionMethod, object[] parameters, Exception exception)
        {
            throw exception;
        }
    }
}
