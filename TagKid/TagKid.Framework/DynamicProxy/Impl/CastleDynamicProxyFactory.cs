using Castle.DynamicProxy;
using System;

namespace TagKid.Framework.DynamicProxy.Impl
{
    public class CastleDynamicProxyFactory : IDynamicProxyFactory
    {
        private readonly ProxyGenerator _proxyGenerator;

        public CastleDynamicProxyFactory()
        {
            _proxyGenerator = new ProxyGenerator();
        }

        public object CreateClassProxy(Type classType, IProxyInterceptor interceptor)
        {
            return _proxyGenerator.CreateClassProxy(classType, new CastleProxyInterceptorAdapter(interceptor));
        }

        public object CreateInterfaceProxyWithoutTarget(Type interfaceType, IProxyInterceptor interceptor)
        {
            return _proxyGenerator.CreateInterfaceProxyWithoutTarget(interfaceType, new CastleProxyInterceptorAdapter(interceptor));
        }

        public object CreateInterfaceProxyWithTarget(Type interfaceType, object target, IProxyInterceptor interceptor)
        {
            return _proxyGenerator.CreateInterfaceProxyWithTarget(interfaceType, target, new CastleProxyInterceptorAdapter(interceptor));
        }
    }
}
