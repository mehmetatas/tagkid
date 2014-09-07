using System.Runtime.Serialization;

namespace TagKid.Lib.Models.DTO.Messages
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "responseCode")]
        public int ResponseCode { get; set; }

        [DataMember(Name = "responseMessage")]
        public string ResponseMessage { get; set; }   
    }
}