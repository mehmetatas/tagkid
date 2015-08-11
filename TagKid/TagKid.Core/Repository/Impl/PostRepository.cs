using System.Linq;
using TagKid.Core.Models.Database;
using TagKid.Framework.UnitOfWork;

namespace TagKid.Core.Repository.Impl
{
    public class PostRepository : IPostRepository
    {
        private readonly IRepository _db;

        public PostRepository(IRepository db)
        {
            _db = db;
        }

        public Post GetPostById(long id)
        {
            return _db.GetById<Post>(id);
        }

        public void Save(Post post)
        {
            if (post.Id > 0)
            {
                _db.DeleteMany<PostTag>(pt => pt.Post.Id == post.Id);
                _db.Update(post);
            }
            else
            {
                _db.Insert(post);
            }

            var tagNames = post.Tags.Select(t => t.Name).ToArray();

            var existingTags = _db.Select<Tag>()
                .Where(t => tagNames.Contains(t.Name))
                .ToList();

            foreach (var tag in post.Tags)
            {
                var existingTag = existingTags.FirstOrDefault(t => t.Name == tag.Name);
                if (existingTag == null)
                {
                    tag.Count = 1;
                    _db.Insert(tag);
                }
                else
                {
                    tag.Id = existingTag.Id;
                    existingTag.Count += 1;
                    _db.Update(existingTag);
                }
            }

            foreach (var tag in post.Tags)
            {
                _db.Insert(new PostTag
                {
                    Post = post,
                    Tag = tag
                });
            }
        }
    }
}
