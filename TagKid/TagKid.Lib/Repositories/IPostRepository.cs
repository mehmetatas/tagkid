using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using TagKid.Lib.Models.Filters;

namespace TagKid.Lib.Repositories
{
    public interface IPostRepository : ITagKidRepository
    {
        PostView GetById(long postId);

        IPage<PostView> GetForUserId(long userId, int pageIndex, int pageSize);

        IPage<PostView> Search(PostFilter filter);

        void Save(Post post, params Tag[] tags);
    }
}