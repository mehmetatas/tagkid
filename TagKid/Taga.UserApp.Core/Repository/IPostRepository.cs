using Taga.Core.Repository;
using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Core.Repository
{
    public interface IPostRepository : IUserAppRepository
    {
        void Save(Category category);

        void Save(Post post);

        Category GetCategory(long categoryId);

        Post GetPost(long postId);

        Category[] GetCategoriesOfUser(long userId);

        void Delete(Post post);

        void Delete(Category category);

        IPage<Post> GetPostsOfUser(long userId, int pageIndex = 1, int pageSize = 10);

        IPage<Post> GetPostsOfCategory(long catId, int pageIndex = 1, int pageSize = 10);
    }
}
