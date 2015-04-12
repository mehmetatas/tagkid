namespace TagKid.Core.Models.DTO.Messages.Post
{
    public class CommentRequest
    {
        public long PostId { get; set; }
        public string Comment { get; set; }
    }
}