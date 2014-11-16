
using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.Domain
{
    public class AudioPost: PostProxy
    {
        public AudioPost()
        {
        }

        public AudioPost(Post post)
            : base(post)
        {
        }

        public string AudioUrl
        {
            get { return Post.MediaEmbedUrl; }
            set { Post.MediaEmbedUrl = value; }
        }
    }
}
