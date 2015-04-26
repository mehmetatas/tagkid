using TagKid.Core.Models.Database;

namespace TagKid.Core.Domain
{
    public interface IPostDomain
    {
        void Save(Post post);
    }
}
