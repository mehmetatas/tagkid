﻿
namespace TagKid.Framework.Hosting
{
    public interface IActionInterceptorBuilder
    {
        IActionInterceptor Build(RouteContext context);
    }
}
