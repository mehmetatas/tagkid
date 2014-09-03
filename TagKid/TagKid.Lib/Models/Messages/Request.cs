using System.Runtime.Serialization;

namespace TagKid.Lib.Models.Messages
{
    [DataContract]
    public abstract class Request
    {
        [DataMember(Name = "requestToken")]
        public string RequestToken { get; set; }
    }
}
