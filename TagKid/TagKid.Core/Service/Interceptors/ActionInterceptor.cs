using System;
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
                var tokenHeader = ctx.GetRequestHeader(AuthToken);
                var tokenIdHeader = ctx.GetRequestHeader(AuthTokenId);

                if (String.IsNullOrWhiteSpace(tokenHeader) || String.IsNullOrWhiteSpace(tokenIdHeader))
                {
                    throw new TagKidException(Errors.S_ActionRequiresAuth);
                }

                var authToken = new Guid(tokenHeader);
                var authTokenId = Convert.ToInt64(tokenIdHeader);

                var authDomainService = _prov.GetOrCreate<IAuthDomainService>();
                authDomainService.SetupRequestContext(authTokenId, authToken);
            }

            foreach (var parameter in parameters)
            {
                TagKidValidator.Validate(parameter);
            }
        }

        public void AfterCall(IRequestContext ctx, MethodInfo actionMethod, object[] parameters, object returnValue)
        {
            _uow.Save(true);

            var token = RequestContext.AuthToken;
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
                return Response.Error(Errors.Unknown);
            }

            if (tkException.Error.Type == ErrorType.Security)
            {
                _uow.Save(true);
            }

            return Response.Error(tkException.Error);
        }

        private static void FlushLogs()
        {
            L.Flush(LogLevel.Debug, LogLevel.Warning);
        }
    }
}
