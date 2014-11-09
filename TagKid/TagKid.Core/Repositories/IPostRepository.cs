using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Filters;

namespace TagKid.Core.Repositories
{
    public interface IPostRepository : ITagKidRepository
    {
        Post GetById(long postId);

        IPage<Post> GetForUserId(long userId, int pageIndex, int pageSize);

        IPage<Post> Search(PostFilter filter);

        void Save(Post post);
    }
}