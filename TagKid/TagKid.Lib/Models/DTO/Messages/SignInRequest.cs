
namespace TagKid.Lib.Models.DTO.Messages
{
    public class SignInRequest : Request
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
        public string FacebookId { get; set; }
        public string FacebookToken { get; set; }
    }
}