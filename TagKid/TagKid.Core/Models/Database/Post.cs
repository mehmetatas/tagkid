using System;
using System.Collections.Generic;
using TagKid.Framework.Repository;

namespace TagKid.Core.Models.Database
{
    public class Post : IEntity
    {
        public virtual long Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? PublishDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual string Title { get; set; }
        public virtual string HtmlContent { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }

        public virtual List<Tag> Tags { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}