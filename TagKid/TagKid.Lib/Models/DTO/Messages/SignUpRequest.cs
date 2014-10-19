
namespace TagKid.Lib.Models.DTO.Messages
{
    public class SignUpRequest : Request
    {
        public SignUpUserModel User { get; set; }
        public string FacebookToken { get; set; }
    }

    public class SignUpUserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FacebookId { get; set; }
    }
}