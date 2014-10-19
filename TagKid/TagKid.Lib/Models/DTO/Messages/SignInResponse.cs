namespace TagKid.Lib.Models.DTO.Messages
{
    public class SignInResponse : Response
    {
        public long AuthTokenId { get; set; }
        public string AuthToken { get; set; }
        public long UserId { get; set; }
        public PublicUserModel User { get; set; }
    }
}