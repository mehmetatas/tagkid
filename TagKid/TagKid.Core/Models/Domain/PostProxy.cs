using System;
using System.Collections.Generic;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.Domain
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

        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public long Id
        {
            get { return Post.Id; }
            set { Post.Id = value; }
        }

        public long UserId
        {
            get { return Post.UserId; }
            set { Post.UserId = value; }
        }

        public long CategoryId
        {
            get { return Post.CategoryId; }
            set { Post.CategoryId = value; }
        }

        public DateTime CreateDate
        {
            get { return Post.CreateDate; }
            set { Post.CreateDate = value; }
        }

        public DateTime PublishDate
        {
            get { return Post.PublishDate; }
            set { Post.PublishDate = value; }
        }

        public DateTime UpdateDate
        {
            get { return Post.UpdateDate; }
            set { Post.UpdateDate = value; }
        }

        public string Title
        {
            get { return Post.Title; }
            set { Post.Title = value; }
        }

        public string ContentCode
        {
            get { return Post.ContentCode; }
            set { Post.ContentCode = value; }
        }

        public string Content
        {
            get { return Post.Content; }
            set { Post.Content = value; }
        }

        public PostType Type
        {
            get { return Post.Type; }
            set { Post.Type = value; }
        }

        public AccessLevel AccessLevel
        {
            get { return Post.AccessLevel; }
            set { Post.AccessLevel = value; }
        }

        public PostStatus Status
        {
            get { return Post.Status; }
            set { Post.Status = value; }
        }

        public User User
        {
            get { return Post.User; }
            set { Post.User = value; }
        }

        public Category Category
        {
            get { return Post.Category; }
            set { Post.Category = value; }
        }

        public List<Tag> Tags
        {
            get { return Post.Tags; }
            set { Post.Tags = value; }
        }

        public static PostProxy Create(Post post)
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