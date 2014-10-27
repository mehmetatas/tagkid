
namespace Taga.UserApp.Core.Model.Database
{
    public class ImagePost
    {
        public virtual long PostId { get; set; }
        public virtual byte[] Bytes { get; set; }
        public virtual string Description { get; set; }
    }
}
