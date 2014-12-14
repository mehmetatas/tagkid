using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Taga.Core.IoC;
using Taga.Core.Logging;
using Taga.Core.Repository;
using Taga.Core.Rest;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Logging;
using TagKid.Core.Models;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Validation;
using IServiceProvider = Taga.Core.IoC.IServiceProvider;

namespace TagKid.Core.Service.Interceptors
{
    public class ActionInterceptor : IActionInterceptor
    {
        private const string AuthToken = "tagkid-auth-token";
        private const string AuthTokenId = "tagkid-auth-token-id";

        private readonly IServiceProvider _prov;
        private ITransactionalUnitOfWork _uow;

        public ActionInterceptor()
        {
            _prov = ServiceProvider.Provider;
        }

        public void Dispose()
        {
            if (_uow != null)
            {
                _uow.Dispose();
            }
        }

        public void BeforeCall(IRequestContext ctx, MethodInfo actionMethod, object[] parameters)
        {
            _uow = _prov.GetOrCreate<ITransactionalUnitOfWork>();
            _uow.BeginTransaction();

            if (!NoAuthMethods.Contains(actionMethod))
            {
                var authToken = new Guid(ctx.GetRequestHeader(AuthToken));
                var authTokenId = Convert.ToInt64(ctx.GetRequestHeader(AuthTokenId));

                var authDomainService = _prov.GetOrCreate<IAuthDomainService>();
                authDomainService.SetupRequestContext(authTokenId, authToken);
            }

            foreach (var parameter in parameters)
            {
                Validator.Validate(parameter);
            }
        }

        public void AfterCall(IRequestContext ctx, MethodInfo actionMethod, object[] parameters, object returnValue)
        {
            _uow.Save(true);

            var token = RequestContext.Current.AuthToken;
            if (token != null)
            {
                ctx.SetResponseHeader(AuthToken, token.Guid.ToString());
                ctx.SetResponseHeader(AuthTokenId, token.Id.ToString());
            }

            FlushLogs();
        }

        public object OnException(IRequestContext ctx, MethodInfo actionMethod, object[] parameters, Exception exception)
        {
            L.Err(String.Format("Service call ended with exception. Service: {0}, Method: {1}",
                actionMethod.DeclaringType.Name, actionMethod.Name), exception);

            FlushLogs();

            var tkException = exception as TagKidException;

            if (tkException == null)
            {
                return new Response
                {
                    ResponseCode = Errors.Unknown.Code,
                    ResponseMessage = Errors.Unknown.Message
                };
            }

            if (tkException.Error.Type == ErrorType.Security)
            {
                _uow.Save(true);
            }

            return new Response
            {
                ResponseCode = tkException.Error.Code,
                ResponseMessage = tkException.Error.Message
            };
        }

        private static void FlushLogs()
        {
            L.Flush(LogLevel.Debug, LogLevel.Warning);
        }

        private static readonly HashSet<MethodInfo> NoAuthMethods = new HashSet<MethodInfo>();

        static ActionInterceptor()
        {
            new NoAuth<IAuthService>()
                .Add(s => s.SignUpWithEmail(null))
                .Add(s => s.SignInWithPassword(null))
                .Add(s => s.SignInWithToken(null))
                .Add(s => s.ActivateAccount(null));
        }

        private class NoAuth<TService>
        {
            internal NoAuth<TService> Add(Expression<Action<TService>> expression)
            {
                var methodCall = (MethodCallExpression)expression.Body;
                NoAuthMethods.Add(methodCall.Method);
                return this;
            }
        }
    }
}
