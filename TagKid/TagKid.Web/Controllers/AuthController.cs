using System;
using System.Web.Http;
using Taga.Core.IoC;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Services;

namespace TagKid.Web.Controllers
{
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
        public Response SignUpWithEmail([FromBody]SignUpRequest request)
        {
            request.Fullname = request.Username;
            request.FacebookId = String.Empty;
            return RegisterUser(request);
        }

        [HttpPost]
        [ActionName("signup_facebook")]
        public Response SignUpWithFacebook([FromBody]SignUpRequest request)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "signup_facebook not implemented!"
            };
        }

        [HttpPost]
        [ActionName("check_email")]
        public Response CheckEmail([FromBody]string email)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "check_email not implemented!"
            };
        }

        [HttpPost]
        [ActionName("check_username")]
        public Response CheckUsername([FromBody]string username)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "check_username not implemented!"
            };
        }

        [HttpPost]
        [ActionName("signin_email")]
        public Response SignInWithEmail([FromBody]SignInRequest request)
        {
            try
            {
                _authService.SignIn(request);

                return new Response
                {
                    ResponseMessage = "Registration OK!"
                };
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
        public Response SignInWithFacebook([FromBody]SignInRequest request)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "signin_facebook not implemented!"
            };
        }

        [HttpPost]
        [ActionName("forgot_password")]
        public Response ForgotPassword([FromBody]string emailOrUsername)
        {
            return new Response
            {
                ResponseCode = -1,
                ResponseMessage = "forgot_password not implemented!"
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
                    ResponseMessage = "Registration OK!"
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
