using System.Runtime.Serialization;

namespace TagKid.Lib.Models.DTO.Messages
{
    [DataContract]
    public class TagSearchResponse : Response
    {
        [DataMember(Name = "tags")]
        public TagModel[] Tags { get; set; }
    }
}
