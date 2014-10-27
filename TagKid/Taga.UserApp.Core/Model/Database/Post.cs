using System;

namespace Taga.UserApp.Core.Model.Database
{
    public class Post
    {
        public virtual long Id { get; set; }
        public virtual long CategoryId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual PostStatus Status { get; set; }
    }
}
