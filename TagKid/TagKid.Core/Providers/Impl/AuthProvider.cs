using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using TagKid.Core.Context;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Framework.Repository;
using TagKid.Framework.WebApi;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.Core.Providers.Impl
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IRepository _repo;

        public AuthProvider(IRepository repo)
        {
            _repo = repo;
        }

        public void AuthRoute(RouteContext ctx)
        {
            var tokenGuid = Guid.Empty;

            IEnumerable<string> headerValues;
            if (ctx.Request.Headers.TryGetValues("tagkid-auth-token", out headerValues))
            {
                var authToken = headerValues.FirstOrDefault();
                Guid.TryParse(authToken, out tokenGuid);
            }

            if (tokenGuid == Guid.Empty)
            {
                if (NoAuthMethods.Contains(ctx.Method.Method))
                {
                    return;
                }

                throw Errors.Auth_LoginRequired.ToException();
            }

            var token = _repo.Select<Token>()
                .Fetch(t => t.User)
                .FirstOrDefault(t => t.Guid == tokenGuid);

            if (token == null)
            {
                throw Errors.Auth_LoginRequired.ToException();
            }

            if (token.ExpireDate < DateTime.UtcNow)
            {
                throw Errors.Auth_LoginTokenExpired.ToException();
            }

            if (token.User.Status == UserStatus.AwaitingActivation)
            {
                throw Errors.Auth_UserAwaitingActivation.ToException();
            }

            if (token.User.Status == UserStatus.Passive)
            {
                throw Errors.Auth_UserInactive.ToException();
            }

            if (token.User.Status == UserStatus.Banned)
            {
                throw Errors.Auth_UserBanned.ToException();
            }

            TagKidContext.Current.User = token.User;
        }
    }
}
