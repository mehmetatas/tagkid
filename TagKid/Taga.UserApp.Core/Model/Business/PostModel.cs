using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Core.Model.Business
{
    public class PostModel : Post
    {
        public CategoryModel Category { get; set; }
    }
}
