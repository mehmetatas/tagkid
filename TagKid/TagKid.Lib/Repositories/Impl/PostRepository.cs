using System;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using TagKid.Lib.Models.Filters;

namespace TagKid.Lib.Repositories.Impl
{
    public class PostRepository : IPostRepository
    {
        private readonly IRepository _repository;

        public PostRepository(IRepository repository)
        {
            _repository = repository;
        }

        public PostView GetById(long postId)
        {
            return _repository.Select<PostView>()
                .FirstOrDefault(p => p.Id == postId);
        }

        public IPage<PostView> GetForUserId(long userId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IPage<PostView> Search(PostFilter filter)
        {
            throw new NotImplementedException();
        }

        public void Save(Post post, params Tag[] tags)
        {
            _repository.Save(post);

            foreach (var tag in tags)
            {
                _repository.Save(new PostTag {PostId = post.Id, TagId = tag.Id});
                _repository.Save(new TagPost {PostId = post.Id, TagId = tag.Id});
            }
        }
    }
}