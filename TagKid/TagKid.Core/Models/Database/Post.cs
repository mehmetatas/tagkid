using System;
using System.Collections.Generic;

namespace TagKid.Core.Models.Database
{
    public class Post
    {
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? PublishDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual string Title { get; set; }
        public virtual EditorType EditorType { get; set; }
        public virtual string EditorContent { get; set; }
        public virtual string HtmlContent { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual PostStatus Status { get; set; }

        public virtual User User { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}