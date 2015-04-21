using System;
using System.Reflection;

namespace TagKid.Framework.DynamicProxy
{
    public interface IInvocationContext
    {
        Type ServiceType { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        object ReturnValue { get; set; }

        void Proceed();
    }
}
