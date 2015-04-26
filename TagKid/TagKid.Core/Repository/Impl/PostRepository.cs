using System.Linq;
using TagKid.Core.Models.Database;
using TagKid.Framework.Repository;

namespace TagKid.Core.Repository.Impl
{
    public class PostRepository : IPostRepository
    {
        private readonly IRepository _repo;
        private readonly IAdoRepository _adoRepo;

        public PostRepository(IRepository repo, IAdoRepository adoRepo)
        {
            _repo = repo;
            _adoRepo = adoRepo;
        }

        public Post GetPostById(long id)
        {
            return _repo.Get<Post>(id);
        }

        public void Save(Post post)
        {
            if (post.Id > 0)
            {
                _adoRepo.Delete<PostTag, Post>(pt => pt.Post, post);
            }

            _repo.Save(post);

            var tagNames = post.Tags.Select(t => t.Name).ToArray();

            var existingTags = _repo.Select<Tag>()
                .Where(t => tagNames.Contains(t.Name))
                .ToList();

            foreach (var tag in post.Tags)
            {
                var existingTag = existingTags.FirstOrDefault(t => t.Name == tag.Name);
                if (existingTag == null)
                {
                    tag.Count = 1;
                    _repo.Insert(tag);
                }
                else
                {
                    tag.Id = existingTag.Id;
                    existingTag.Count += 1;
                    _repo.Update(existingTag);
                }
            }

            foreach (var tag in post.Tags)
            {
                _repo.Insert(new PostTag
                {
                    Post = post,
                    Tag = tag
                });
            }
        }
    }
}
