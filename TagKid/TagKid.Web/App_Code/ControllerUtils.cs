using System;
using System.Web;
using Taga.Core.IoC;
using TagKid.Lib.Models.DTO;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Services;

namespace TagKid.Web
{
    public static class ControllerUtils
    {
        public static void ValidateRequest(Request request)
        {
            var cookies = HttpContext.Current.Request.Cookies;

            var authToken = cookies.GetValue("authToken") ?? Guid.Empty.ToString();
            var authTokenId = cookies.GetValue("authTokenId") ?? "-1";
            var requestToken = cookies.GetValue("requestToken") ?? Guid.Empty.ToString();
            var requestTokenId = cookies.GetValue("requestTokenId") ?? "-1";

            request.Context = new RequestContext
            {
                AuthToken = new Token
                {
                    Id = Convert.ToInt64(authTokenId),
                    Guid = new Guid(authToken),
                    Type = TokenType.Auth
                },
                RequestToken = new Token
                {
                    Id = Convert.ToInt64(requestTokenId),
                    Guid = new Guid(requestToken),
                    Type = TokenType.Request
                }
            };

            ServiceProvider.Provider.GetOrCreate<IAuthService>().ValidateRequest(request);
        }

        public static void FinalizeResponse(Request request, Response response)
        {
            var newToken = request.Context.NewRequestToken;
            if (newToken != null)
            {
                response.RequestTokenId = newToken.Id;
                response.RequestToken = newToken.Guid.ToString();
            }
        }

        private static string GetValue(this HttpCookieCollection cookies, string key)
        {
            return cookies[key] != null
                ? cookies[key].Value
                : null;
        }
    }
}