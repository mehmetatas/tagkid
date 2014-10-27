
namespace Taga.UserApp.Core.Model.Database
{
    public class QuotePost
    {
        public virtual long PostId { get; set; }
        public virtual string Author { get; set; }
        public virtual string Quote { get; set; }
    }
}
