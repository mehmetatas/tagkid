using System.Runtime.Serialization;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Models.DTO
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "fullname")]
        public string Fullname { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "profileImageUrl")]
        public string ProfileImageUrl { get; set; }

        [DataMember(Name = "facebookId")]
        public string FacebookId { get; set; }

        [DataMember(Name = "type")]
        public UserType Type { get; set; }

        [DataMember(Name = "status")]
        public UserStatus Status { get; set; }
    }
}
