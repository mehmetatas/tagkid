using System.Runtime.Serialization;

namespace TagKid.Lib.Models.Messages
{
    [DataContract]
    public class SignInRequest : Request
    {
        [DataMember(Name = "emailOrUsername")]
        public string EmailOrUsername { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "facebookId")]
        public string FacebookId { get; set; }

        [DataMember(Name = "facebookToken")]
        public string FacebookToken { get; set; }
    }
}