using System;
using System.Linq;
using Taga.Core.Repository;
using Taga.UserApp.Core.Model.Database;
using Taga.UserApp.Core.Repository;

namespace Taga.UserApp.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IRepository _repository;

        public PostRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void Save(Category category)
        {
            _repository.Save(category);
        }

        public void Save(Post post)
        {
            _repository.Save(post);
        }

        public Category GetCategory(long categoryId)
        {
            return _repository.Select<Category>()
                .SingleOrDefault(c => c.Id == categoryId);
        }

        public Post GetPost(long postId)
        {
            throw new NotImplementedException();
        }

        public Category[] GetCategoriesOfUser(long userId)
        {
            return _repository.Select<Category>()
                .Where(c => c.UserId == userId)
                .ToArray();
        }

        public void Delete(Post post)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category category)
        {
            _repository.Delete(category);
        }

        public IPage<Post> GetPostsOfUser(long userId, int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public IPage<Post> GetPostsOfCategory(long catId, int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
