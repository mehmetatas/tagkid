
namespace Taga.UserApp.Core.Model.Business
{
    public class QuotePostModel : PostModel
    {
        public virtual string Author { get; set; }
        public virtual string Quote { get; set; }
    }
}
