namespace TagKid.Core.Models.Messages.Auth
{
    public class SignupRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
    }
}
