using System.Runtime.Serialization;

namespace TagKid.Lib.Models.DTO.Messages
{
    [DataContract]
    public class TagSearchRequest : Request
    {
        [DataMember(Name = "filter")]
        public string Filter { get; set; }
    }
}
