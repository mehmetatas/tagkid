namespace TagKid.Lib.Models.DTO.Messages
{
    public class SignUpRequest : Request
    {
        public SignUpUserModel User { get; set; }
        public string FacebookToken { get; set; }
    }
}