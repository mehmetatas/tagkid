using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.Domain
{
    public class ImagePost : PostProxy
    {
        public ImagePost()
        {
        }

        public ImagePost(Post post)
            : base(post)
        {
        }

        public string ImageUrl
        {
            get { return Post.MediaEmbedUrl; }
            set { Post.MediaEmbedUrl = value; }
        }
    }
}