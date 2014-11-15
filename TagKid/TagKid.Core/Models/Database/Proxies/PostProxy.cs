using System;
using System.Collections.Generic;

namespace TagKid.Core.Models.Database.Proxies
{
    public abstract class PostProxy
    {
        public readonly Post Post;

        protected PostProxy()
            : this(new Post())
        {
        }

        protected PostProxy(Post post)
        {
            Post = post;
        }

        public virtual long Id
        {
            get { return Post.Id; }
            set { Post.Id = value; }
        }

        public virtual long UserId
        {
            get { return Post.UserId; }
            set { Post.UserId = value; }
        }

        public virtual long CategoryId
        {
            get { return Post.CategoryId; }
            set { Post.CategoryId = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return Post.CreateDate; }
            set { Post.CreateDate = value; }
        }

        public virtual DateTime PublishDate
        {
            get { return Post.PublishDate; }
            set { Post.PublishDate = value; }
        }

        public virtual DateTime UpdateDate
        {
            get { return Post.UpdateDate; }
            set { Post.UpdateDate = value; }
        }

        public virtual string Title
        {
            get { return Post.Title; }
            set { Post.Title = value; }
        }

        public virtual string ContentCode
        {
            get { return Post.ContentCode; }
            set { Post.ContentCode = value; }
        }

        public virtual string Content
        {
            get { return Post.Content; }
            set { Post.Content = value; }
        }

        public virtual PostType Type
        {
            get { return Post.Type; }
            set { Post.Type = value; }
        }

        public virtual AccessLevel AccessLevel
        {
            get { return Post.AccessLevel; }
            set { Post.AccessLevel = value; }
        }

        public virtual PostStatus Status
        {
            get { return Post.Status; }
            set { Post.Status = value; }
        }

        public virtual User User
        {
            get { return Post.User; }
            set { Post.User = value; }
        }

        public virtual Category Category
        {
            get { return Post.Category; }
            set { Post.Category = value; }
        }

        public virtual List<Tag> Tags
        {
            get { return Post.Tags; }
            set { Post.Tags = value; }
        }

        public static PostProxy For(Post post)
        {
            switch (post.Type)
            {
                case PostType.Text:
                    return new TextPost(post);
                case PostType.Quote:
                    return new QuotePost(post);
                case PostType.Image:
                    return new ImagePost(post);
                case PostType.Audio:
                    return new AudioPost(post);
                case PostType.Video:
                    return new VideoPost(post);
                case PostType.Link:
                    return new LinkPost(post);
                case PostType.Retag:
                    return new RetagPost(post);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}