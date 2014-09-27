using System.Runtime.Serialization;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Models.DTO
{
    [DataContract]
    public class PublicUserModel
    {
        public PublicUserModel()
        {
        }

        public PublicUserModel(User entity)
        {
            Username = entity.Username;
            Fullname = entity.Fullname;
            ProfileImageUrl = entity.ProfileImageUrl;
        }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "fullname")]
        public string Fullname { get; set; }

        [DataMember(Name = "profileImageUrl")]
        public string ProfileImageUrl { get; set; }
    }
}
