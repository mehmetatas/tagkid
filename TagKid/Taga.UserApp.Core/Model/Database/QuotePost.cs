
namespace Taga.UserApp.Core.Model.Database
{
    public class QuotePost : Post
    {
        public virtual string QuoteAuthor { get; set; }
        public virtual string QuoteText { get; set; }
    }
}
