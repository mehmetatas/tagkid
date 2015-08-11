using System;
using System.Collections.Generic;
using System.Linq;
using DummyOrm.Db;
using TagKid.Core.Context;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Framework.WebApi;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.Core.Providers.Impl
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IDbFactory _dbFactory;

        public AuthProvider(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void AuthRoute(RouteContext ctx)
        {
            using (var db = _dbFactory.Create())
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
                    if (ctx.Method.NoAuth)
                    {
                        return;
                    }

                    throw Errors.Auth_LoginRequired;
                }

                var token = db.Select<Token>()
                    .Join(t => t.User)
                    .FirstOrDefault(t => t.Guid == tokenGuid);

                if (token == null)
                {
                    throw Errors.Auth_LoginRequired;
                }

                if (token.ExpireDate < DateTime.UtcNow)
                {
                    throw Errors.Auth_LoginTokenExpired;
                }

                if (token.User.Status == UserStatus.AwaitingActivation)
                {
                    throw Errors.Auth_UserAwaitingActivation;
                }

                if (token.User.Status == UserStatus.Passive)
                {
                    throw Errors.Auth_UserInactive;
                }

                if (token.User.Status == UserStatus.Banned)
                {
                    throw Errors.Auth_UserBanned;
                }

                TagKidContext.Current.User = token.User;
            }
        }
    }
}
