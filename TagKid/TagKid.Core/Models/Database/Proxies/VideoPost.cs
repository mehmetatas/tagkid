namespace TagKid.Core.Models.Database.Proxies
{
    public class VideoPost : PostProxy
    {
        public VideoPost()
        {
        }

        public VideoPost(Post post)
            : base(post)
        {
        }

        public virtual string VideoUrl
        {
            get { return Post.MediaEmbedUrl; }
            set { Post.MediaEmbedUrl = value; }
        }
    }
}