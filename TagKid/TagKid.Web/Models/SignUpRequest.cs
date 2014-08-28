using System.Runtime.Serialization;

namespace TagKid.Web.Models
{
    [DataContract]
    public class SignUpRequest
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "facebook_id")]
        public string FacebookId { get; set; }

        [DataMember(Name = "facebook_token")]
        public string FacebookToken { get; set; }

        [DataMember(Name = "fullname")]
        public string FullName { get; set; }
    }
}