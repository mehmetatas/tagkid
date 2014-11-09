namespace TagKid.Core.Repositories
{
    public interface IRepositoryProvider
    {
        TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository;
    }
}