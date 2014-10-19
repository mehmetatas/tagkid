using System;
using System.Web;
using System.Web.Http;
using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Services;

namespace TagKid.Web.Controllers
{
    [Intercept]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController()
        {
            try
            {
                _authService = ServiceProvider.Provider.GetOrCreate<IAuthService>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetMessage());
            }
        }

        [HttpPost]
        [ActionName("signup_email")]
        public virtual Response SignUpWithEmail([FromBody] SignUpRequest request)
        {
            return RegisterUser(request);
        }

        [HttpPost]
        [ActionName("signup_facebook")]
        public virtual Response SignUpWithFacebook([FromBody] SignUpRequest request)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "signup_facebook not implemented!"
            };
        }

        [HttpPost]
        [ActionName("check_email")]
        public virtual Response CheckEmail([FromBody] string email)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "check_email not implemented!"
            };
        }

        [HttpPost]
        [ActionName("check_username")]
        public virtual Response CheckUsername([FromBody] string username)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "check_username not implemented!"
            };
        }

        [HttpPost]
        [ActionName("signin_email")]
        public virtual Response SignInWithEmail([FromBody] SignInRequest request)
        {
            try
            {
                return _authService.SignIn(request);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = -1,
                    ResponseMessage = ex.GetMessage()
                };
            }
        }

        [HttpPost]
        [ActionName("signin_facebook")]
        public virtual Response SignInWithFacebook([FromBody] SignInRequest request)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "signin_facebook not implemented!"
            };
        }

        [HttpPost]
        [ActionName("forgot_password")]
        public virtual Response ForgotPassword([FromBody] string emailOrUsername)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "forgot_password not implemented!"
            };
        }

        [HttpPost]
        [ActionName("validate_auth_cookie")]
        public virtual Response ValidateAuthCookie()
        {
            if (HttpContext.Current.Request.Cookies["authToken"] != null &&
                HttpContext.Current.Request.Cookies["authTokenId"] != null)
            {
                var authToken = HttpContext.Current.Request.Cookies["authToken"].Value;
                long authTokenId;

                if (!String.IsNullOrEmpty(authToken) &&
                    Int64.TryParse(HttpContext.Current.Request.Cookies["authTokenId"].Value, out authTokenId))
                {
                    return _authService.SignInWithToken(authTokenId, authToken);
                }
            }

            return new SignInResponse
            {
                ResponseCode = -1,
                AuthToken = null,
                RequestToken = null
            };
        }

        [NonAction]
        private Response RegisterUser(SignUpRequest request)
        {
            try
            {
                _authService.SignUp(request);

                return new Response
                {
                    ResponseMessage = "Signup OK!"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = -1,
                    ResponseMessage = ex.Message
                };
            }
        }
    }
}