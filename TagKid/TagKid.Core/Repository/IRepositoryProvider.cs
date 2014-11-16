namespace TagKid.Core.Repository
{
    public interface IRepositoryProvider
    {
        TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository;
    }
}