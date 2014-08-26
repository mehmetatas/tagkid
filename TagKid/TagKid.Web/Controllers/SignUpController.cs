using System;
using System.Web.Http;
using TagKid.Lib.Entities;
using TagKid.Lib.Service;
using TagKid.Web.Models;

namespace TagKid.Web.Controllers
{
    public class SignUpController : ApiController
    {
        public Response Post([FromBody]SignUpModel user)
        {
            try
            {
                var svc = new TagKidService();
                svc.RegisterUser(new User
                {
                    Email = user.Email,
                    FacebookId = user.FacebookId,
                    FullName = user.FullName,
                    Password = user.Password,
                    Username = user.Username
                });

                return new Response
                {
                    IsSuccessful = true,
                    Message = "Registration OK!"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
