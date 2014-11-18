using System;
using System.Collections.Generic;
using System.Reflection;
using Taga.Core.Logging;
using Taga.Core.Rest;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Logging;
using TagKid.Core.Models;
using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Service.Interceptors
{
    public class ActionInterceptor : IActionInterceptor
    {
        private readonly IDomainServiceProvider _prov;

        public ActionInterceptor(IDomainServiceProvider prov)
        {
            _prov = prov;
        }

        public void BeforeCall(IRequestContext ctx, MethodInfo actionMethod, object[] parameters)
        {
            if (NoAuthMethods.Contains(actionMethod))
            {
                return;
            }

            var authDomainService = _prov.GetService<IAuthDomainService>();

            var authToken = ctx.GetHeader("tagkid-auth-token");
            var authTokenId = Convert.ToInt64(ctx.GetHeader("tagkid-auth-token-id"));

            RequestContext.Current.User = authDomainService.ValidateAuthToken(authTokenId, authToken);
        }

        public void AfterCall(IRequestContext ctx, MethodInfo actionMethod, object[] parameters, object returnValue)
        {
            FlushLogs();
        }

        public object OnException(IRequestContext ctx, MethodInfo actionMethod, object[] parameters, Exception exception)
        {
            L.E(String.Format("Service call ended with exception. Service: {0}, Method: {1}",
                actionMethod.DeclaringType.Name, actionMethod.Name), exception);

            FlushLogs();

            var tkException = exception as TagKidException;

            if (tkException == null)
            {
                return new Response
                {
                    ResponseCode = -1,
                    ResponseMessage = "Unknown error has occured!"
                };
            }

            return new Response
            {
                ResponseCode = tkException.ErrorCode,
                ResponseMessage = tkException.UserMessage
            };
        }

        private static void FlushLogs()
        {
            L.Flush(LogLevel.Debug, LogLevel.Warning);
        }

        private static readonly HashSet<MethodInfo> NoAuthMethods = new HashSet<MethodInfo>();

        static ActionInterceptor()
        {
            var authServiceType = typeof(IAuthService);

            NoAuthMethods.Add(authServiceType.GetMethod("SignUpWithEmail"));
        }
    }
}
