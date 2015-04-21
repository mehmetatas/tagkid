using System;

namespace TagKid.Framework.DynamicProxy
{
    public interface IDynamicProxyFactory
    {
        object CreateClassProxy(Type classType, IProxyInterceptor interceptor);

        object CreateInterfaceProxyWithoutTarget(Type interfaceType, IProxyInterceptor interceptor);

        object CreateInterfaceProxyWithTarget(Type interfaceType, object target, IProxyInterceptor interceptor);
    }
}
