namespace TagKid.Core.Models.DTO.Messages.Post
{
    public class GetCommentsRequest
    {
        public long PostId { get; set; }
        public long MaxCommentId { get; set; }
    }
}