using System.Runtime.Serialization;

namespace TagKid.Lib.Models.DTO.Messages
{
    [DataContract]
    public class SignInResponse : Response
    {
        [DataMember(Name = "authTokenId")]
        public long AuthTokenId { get; set; }

        [DataMember(Name = "authToken")]
        public string AuthToken { get; set; }

        [DataMember(Name = "userId")]
        public long UserId { get; set; }

        [DataMember(Name = "user")]
        public PublicUserModel User { get; set; }
    }
}
