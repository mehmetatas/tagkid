using Taga.Core.Repository;
using TagKid.Lib.Entities;
using TagKid.Lib.Entities.Filters;
using TagKid.Lib.Entities.Views;

namespace TagKid.Lib.Repositories
{
    public interface IPostRepository
    {
        PostView GetById(long postId);

        IPage<PostView> GetForUserId(long userId, int pageIndex, int pageSize);

        IPage<PostView> Search(PostFilter filter);

        void Save(Post post);
    }
}
