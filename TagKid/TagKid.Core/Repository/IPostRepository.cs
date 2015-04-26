using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface IPostRepository
    {
        Post GetPostById(long id);

        void Save(Post post);
    }
}
