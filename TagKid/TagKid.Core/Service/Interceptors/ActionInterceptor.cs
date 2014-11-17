using System;
using System.Collections.Generic;
using System.Reflection;
using Taga.Core.Logging;
using Taga.Core.Rest;
using TagKid.Core.Database;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Models;
using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Service.Interceptors
{
    public class ActionInterceptor : IActionInterceptor
    {
        private readonly IDomainServiceProvider _prov;
        private ITransactionalDb _db;

        public ActionInterceptor(IDomainServiceProvider prov)
        {
            _prov = prov;
        }

        public void BeforeCall(IRequestContext ctx, MethodInfo actionMethod, object[] parameters)
        {
            _db = Db.Transactional();
            _db.BeginTransaction();

            if (NoAuthMethods.Contains(actionMethod))
            {
                return;
            }

            var authDomainService = _prov.GetService<IAuthDomainService>();

            var authToken = ctx.GetHeader("tagkid-auth-token");
            var authTokenId = Convert.ToInt64(ctx.GetHeader("tagkid-auth-token-id"));

            RequestContext.Current.User = authDomainService.ValidateAuthToken(authTokenId, authToken);

            _db.Save();
        }

        public void AfterCall(IRequestContext ctx, MethodInfo actionMethod, object[] parameters, object returnValue)
        {
            _db.Save(true);
        }

        public object OnException(IRequestContext ctx, MethodInfo actionMethod, object[] parameters, Exception exception)
        {
            var tkException = exception as TagKidException;
            
            if (tkException == null)
            {
                _db.RollbackTransaction();

                return new Response
                {
                    ResponseCode = -1,
                    ResponseMessage = "Unknown error has occured!"
                };
            }

            _db.Save(true);

            LogScope.Current.Flush(LogLevel.Debug, LogLevel.Warning);

            return new Response
            {
                ResponseCode = tkException.ErrorCode,
                ResponseMessage = tkException.UserMessage
            };
        }

        private static readonly HashSet<MethodInfo> NoAuthMethods = new HashSet<MethodInfo>();

        static ActionInterceptor()
        {
            var authServiceType = typeof(IAuthService);

            NoAuthMethods.Add(authServiceType.GetMethod("SignUpWithEmail"));
        }
    }
}
