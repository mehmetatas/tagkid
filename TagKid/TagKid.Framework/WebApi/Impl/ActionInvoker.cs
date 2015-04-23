using System;
using System.Reflection;
using TagKid.Framework.Exceptions;
using TagKid.Framework.IoC;

namespace TagKid.Framework.WebApi.Impl
{
    public class ActionInvoker : IActionInvoker
    {
        private readonly IActionInterceptorBuilder _interceptorBuilder;

        public ActionInvoker(IActionInterceptorBuilder interceptorBuilder)
        {
            _interceptorBuilder = interceptorBuilder;
        }

        public virtual void InvokeAction(RouteContext ctx)
        {
            var serviceInstance = DependencyContainer.Current.Resolve(ctx.Service.ServiceType);

            using (var interceptor = _interceptorBuilder.Build(ctx))
            {
                try
                {
                    var retVal = interceptor.BeforeCall(ctx);

                    if (retVal == null)
                    {
                        retVal = ctx.Method.Method.Invoke(serviceInstance, ctx.Parameters);
                    }
                    
                    interceptor.AfterCall(ctx);

                    ctx.ReturnValue = retVal; 
                }
                catch (Exception ex)
                {
                    ctx.Exception = ex;
                    HandleException(interceptor, ctx);
                }
            }
        }

        private void HandleException(IActionInterceptor interceptor, RouteContext ctx)
        {
            var ex = ctx.Exception;

            while (ex is TargetInvocationException && ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            var result = interceptor.OnException(ctx);

            if (result != null)
            {
                ctx.ReturnValue = result;
                return;
            }

            var knownEx = ex as TagKidException;
            if (knownEx != null)
            {
                throw knownEx;
            }

            throw Errors.Unknown.ToException();
        }
    }
}