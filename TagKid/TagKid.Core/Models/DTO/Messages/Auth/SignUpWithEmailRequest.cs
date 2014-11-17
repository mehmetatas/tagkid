
namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignUpWithEmailRequest : Request
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
    }
}
