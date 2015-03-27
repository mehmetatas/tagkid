using System;
using System.Linq;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.View;
using TagKid.Core.Utils;

namespace TagKid.Core.Models.Domain
{
    public class PostDO : Post
    {
        public PostDO()
        {
            
        }

        public PostDO(Post post, PostInfo info)
        {
            Util.MapTo(post, this);

            if (info == null)
            {
                return;
            }

            LikeCount = info.LikeCount;
            CommentCount = info.CommentCount;
            Liked = info.Liked;
        }

        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public bool Liked { get; set; }

        public string PublishDateText
        {
            get
            {
                if (!PublishDate.HasValue)
                {
                    return String.Empty;
                }

                var ago = DateTime.Now - PublishDate.Value;
                if (ago.TotalSeconds < 10)
                {
                    return "now";
                }
                if (ago.TotalSeconds < 60)
                {
                    return String.Format("{0}s", (int)ago.TotalSeconds);
                }
                if (ago.TotalMinutes < 60)
                {
                    return String.Format("{0}m", (int)ago.TotalMinutes);
                }
                if (ago.TotalHours < 24)
                {
                    return String.Format("{0}h", (int)ago.TotalHours);
                }
                if (ago.TotalDays < 7)
                {
                    return String.Format("{0}d", (int)ago.TotalDays);
                }
                return PublishDate.Value.ToString("dd MMM yy");
            }
        }

        public static PostDO[] Create(Post[] posts, PostInfo[] infos)
        {
            var arr = new PostDO[posts.Length];

            for (var i = 0; i < posts.Length; i++)
            {
                arr[i] = new PostDO(posts[i], infos.FirstOrDefault(inf => inf.PostId == posts[i].Id));
            }

            return arr;
        }
        
        //public virtual long Id { get; set; }
        //public virtual long UserId { get; set; }
        //public virtual long CategoryId { get; set; }
        //public virtual long? RetaggedPostId { get; set; }
        //public virtual DateTime CreateDate { get; set; }
        //public virtual DateTime? PublishDate { get; set; }
        //public virtual DateTime? UpdateDate { get; set; }
        //public virtual string Title { get; set; }
        //public virtual EditorType EditorType { get; set; }
        //public virtual string EditorContent { get; set; }
        //public virtual string HtmlContent { get; set; }
        //public virtual AccessLevel AccessLevel { get; set; }
        //public virtual PostStatus Status { get; set; }

        //public virtual User User { get; set; }
        //public virtual Category Category { get; set; }
        //public virtual List<Tag> Tags { get; set; }
        //public virtual Post RetaggedPost { get; set; }
    }
}
