using System.Runtime.Serialization;

namespace TagKid.Lib.Models.DTO.Messages
{
    [DataContract]
    public class SignUpRequest : Request
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "facebookId")]
        public string FacebookId { get; set; }

        [DataMember(Name = "facebookToken")]
        public string FacebookToken { get; set; }

        [DataMember(Name = "fullname")]
        public string Fullname { get; set; }
    }
}