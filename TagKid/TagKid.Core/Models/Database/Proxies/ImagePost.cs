namespace TagKid.Core.Models.Database.Proxies
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

        public virtual string ImageUrl
        {
            get { return Post.MediaEmbedUrl; }
            set { Post.MediaEmbedUrl = value; }
        }
    }
}