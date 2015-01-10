using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.DTO.Messages.Post
{
    public class CreateCategoryRequest
    {
        public Category Category { get; set; }
    }

    public class GetCommentsRequest
    {
        public long PostId { get; set; }
        public long MaxCommentId { get; set; }
    }

    public class LikeUnlikeRequest
    {
        public long PostId { get; set; }
    }
}
