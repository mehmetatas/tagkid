﻿
namespace TagKid.Framework.DynamicProxy
{
    public interface IProxyInterceptor
    {
        void Intercept(IInvocationContext context);
    }
}
