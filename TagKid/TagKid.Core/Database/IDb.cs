using TagKid.Core.Repository;

namespace TagKid.Core.Database
{
    public interface IDb
    {
        TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository;
    }
}
