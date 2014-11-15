
namespace TagKid.Core.Models.Database.Proxies
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

        public virtual string AudioUrl
        {
            get { return Post.MediaEmbedUrl; }
            set { Post.MediaEmbedUrl = value; }
        }
    }
}
