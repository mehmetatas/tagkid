using System.Runtime.Serialization;

namespace TagKid.Web.Models
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }   
    }

    [DataContract]
    public class Response<T> : Response
    {
        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}