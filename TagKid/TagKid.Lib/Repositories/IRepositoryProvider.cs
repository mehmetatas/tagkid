namespace TagKid.Lib.Repositories
{
    public interface IRepositoryProvider
    {
        TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository;
    }
}