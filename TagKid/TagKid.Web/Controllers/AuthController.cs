using System;
using System.Web.Http;
using TagKid.Lib.Entities;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Service;
using TagKid.Web.Models;

namespace TagKid.Web.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [ActionName("signup_email")]
        public Response SignUpWithEmail([FromBody]SignUpRequest request)
        {
            request.FullName = request.Username;
            request.FacebookId = String.Empty;
            return RegisterUser(request);
        }

        [HttpPost]
        [ActionName("signup_facebook")]
        public Response SignUpWithFacebook([FromBody]SignUpRequest request)
        {
            return new Response
            {
                Code = -1,
                Message = "signup_facebook not implemented!"
            };
        }

        [HttpPost]
        [ActionName("check_email")]
        public Response CheckEmail([FromBody]string email)
        {
            return new Response
            {
                Code = -1,
                Message = "check_email not implemented!"
            };
        }

        [HttpPost]
        [ActionName("check_username")]
        public Response CheckUsername([FromBody]string username)
        {
            return new Response
            {
                Code = -1,
                Message = "check_username not implemented!"
            };
        }

        [HttpPost]
        [ActionName("signin_email")]
        public Response SignInWithEmail([FromBody]SignInRequest request)
        {
            try
            {
                var svc = new TagKidService();

                svc.LoginUser(request.EmailOrUsername, request.Password);

                return new Response
                {
                    Message = "Registration OK!"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = -1,
                    Message = ex.GetMessage()
                };
            }
        }

        [HttpPost]
        [ActionName("signin_facebook")]
        public Response SignInWithFacebook([FromBody]SignInRequest request)
        {
            return new Response
            {
                Code = -1,
                Message = "signin_facebook not implemented!"
            };
        }

        [HttpPost]
        [ActionName("forgot_password")]
        public Response ForgotPassword([FromBody]string emailOrUsername)
        {
            return new Response
            {
                Code = -1,
                Message = "forgot_password not implemented!"
            };
        }

        [NonAction]
        private static Response RegisterUser(SignUpRequest model)
        {
            try
            {
                var svc = new TagKidService();

                svc.RegisterUser(new User
                {
                    Email = model.Email,
                    FacebookId = model.FacebookId,
                    FullName = model.FullName,
                    Username = model.Username,
                    Password = model.Password
                });

                return new Response
                {
                    Message = "Registration OK!"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Code = -1,
                    Message = ex.Message
                };
            }
        }
    }
}
