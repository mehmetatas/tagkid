
namespace Taga.UserApp.Core.Model.Business
{
    public class ImagePostModel : PostModel
    {
        public virtual byte[] Bytes { get; set; }
        public virtual string Description { get; set; }
    }
}
