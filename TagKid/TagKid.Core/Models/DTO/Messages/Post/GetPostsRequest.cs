namespace TagKid.Core.Models.DTO.Messages.Post
{
    public class GetPostsRequest
    {
        public long UserId { get; set; }
        public long MaxPostId { get; set; }
    }

    public class SearchTagsRequest
    {
        public string TagName { get; set; }
    }
}