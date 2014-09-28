using System;
using System.Linq;
using Taga.Core.DynamicProxy;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Services.Impl
{
    [Intercept]
    public class PostService : IPostService
    {
        public virtual PostResponse SavePost(PostRequest postRequest)
        {
            var post = new Post
            {
                ContentCode = postRequest.ContentCode,
                LinkDescription = postRequest.LinkDescription,
                LinkImageUrl = postRequest.LinkImageUrl,
                LinkTitle = postRequest.LinkTitle,
                LinkUrl = postRequest.LinkUrl,
                MediaEmbedUrl = postRequest.MediaEmbedUrl,
                QuoteAuthor = postRequest.QuoteAuthor,
                QuoteText = postRequest.QuoteText,
                Title = postRequest.Title,
                AccessLevel = postRequest.AccessLevel,
                CategoryId = postRequest.CategoryId,
                Id = postRequest.Id,
                RetaggedPostId = postRequest.RetaggedPostId,
                Status = postRequest.Status,
                Type = postRequest.Type,
                UserId = postRequest.Context.User.Id
            };

            post.Content = EditorUtils.ToHtml(post.ContentCode);

            if (post.Id < 1)
            {
                post.CreateDate = DateTime.Now;
            }
            else
            {
                post.UpdateDate = DateTime.Now;
            }

            if (post.Status == PostStatus.Active &&
                post.PublishDate == DateTime.MinValue)
            {
                post.PublishDate = DateTime.Now;
            }

            var tags = postRequest.Tags.Select(t => new Tag
            {
                Id = t.Id,
                Name = t.Name
            });

            using (var uow = Db.UnitOfWork())
            {
                Db.PostRepository().Save(post, tags.ToArray());
                uow.Save();
            }

            return new PostResponse();
        }
    }
}
