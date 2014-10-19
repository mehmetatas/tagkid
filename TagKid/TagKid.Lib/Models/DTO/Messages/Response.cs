
namespace TagKid.Lib.Models.DTO.Messages
{
    public class Response
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public long RequestTokenId { get; set; }
        public string RequestToken { get; set; }
    }
}