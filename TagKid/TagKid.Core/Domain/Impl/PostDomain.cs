using System;
using TagKid.Core.Context;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Core.Domain.Impl
{
    public class PostDomain : IPostDomain
    {
        private readonly IPostRepository _postRepo;

        public PostDomain(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public void Save(Post post)
        {
            if (post.Id > 0)
            {
                var tmp = _postRepo.GetPostById(post.Id);

                tmp.Title = post.Title;
                tmp.HtmlContent = post.HtmlContent;
                tmp.Tags = post.Tags;
                tmp.AccessLevel = post.AccessLevel;
                tmp.UpdateDate = DateTime.UtcNow;

                post = tmp;
            }
            else
            {
                post.CreateDate = DateTime.UtcNow;
            }

            if (!post.PublishDate.HasValue && post.AccessLevel == AccessLevel.Public)
            {
                post.PublishDate = DateTime.UtcNow;
            }

            post.User = TagKidContext.Current.User;

            _postRepo.Save(post);
        }
    }
}
