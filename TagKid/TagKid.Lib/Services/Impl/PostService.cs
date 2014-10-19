using System;
using System.Linq;
using Taga.Core.DynamicProxy;
using TagKid.Lib.Database;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Repositories;
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
            }).ToArray();

            using (var db = new TagKidDb())
            {
                db.BeginTransaction();

                var postRepo = db.GetRepository<IPostRepository>();
                var tagRepo = db.GetRepository<ITagRepository>();

                foreach (var tag in tags.Where(t => t.Id < 1))
                {
                    tag.Hint = tag.Name;
                    tag.Description = tag.Name;
                    tag.Count = 1;
                    tagRepo.Save(tag);
                }

                postRepo.Save(post, tags);

                db.Save();
            }

            return new PostResponse();
        }
    }
}