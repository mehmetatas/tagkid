using System;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Filters;
using TagKid.Core.Repositories;

namespace TagKid.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IRepository _repository;

        public PostRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Post GetById(long postId)
        {
            return _repository.Select<Post>()
                .FirstOrDefault(p => p.Id == postId);
        }

        public IPage<Post> GetForUserId(long userId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IPage<Post> Search(PostFilter filter)
        {
            throw new NotImplementedException();
        }

        public void Save(Post post)
        {
            if (post.Id > 0)
            {
                _repository.Delete<PostTag>(pt => pt.PostId, post.Id);
            }

            _repository.Save(post);

            foreach (var tag in post.Tags.Where(tag => tag.Id < 1))
            {
                _repository.Insert(tag);
            }

            _repository.Flush();

            foreach (var tag in post.Tags)
            {
                _repository.Insert(new PostTag
                {
                    PostId = post.Id,
                    TagId = tag.Id
                });
            }
        }
    }
}