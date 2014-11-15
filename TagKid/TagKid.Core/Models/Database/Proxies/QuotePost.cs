namespace TagKid.Core.Models.Database.Proxies
{
    public class QuotePost : PostProxy
    {
        public QuotePost()
        {
        }

        public QuotePost(Post post)
            : base(post)
        {
        }

        public virtual string Author
        {
            get { return Post.QuoteAuthor; }
            set { Post.QuoteAuthor = value; }
        }

        public virtual string Text
        {
            get { return Post.QuoteText; }
            set { Post.QuoteText = value; }
        }
    }
}