using Taga.Core.Repository;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface IPostRepository
    {
        Post GetById(long postId);

        IPage<Post> GetForUserId(long userId, int pageIndex, int pageSize);

        IPage<Post> SearchByUserId(long userId, int pageIndex, int pageSize);

        IPage<Post> SearchByCategoryId(long categoryId, int pageIndex, int pageSize);

        IPage<Post> SearchByTagIds(long[] tagIds, int pageIndex, int pageSize);

        IPage<Post> SearchByTitle(string title, int pageIndex, int pageSize);

        void Save(Post post);
    }
}
