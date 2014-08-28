using System.Runtime.Serialization;

namespace TagKid.Web.Models
{
    [DataContract]
    public class SignInRequest
    {
        [DataMember(Name = "email_or_username")]
        public string EmailOrUsername { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "facebook_id")]
        public string FacebookId { get; set; }

        [DataMember(Name = "facebook_token")]
        public string FacebookToken { get; set; }
    }
}