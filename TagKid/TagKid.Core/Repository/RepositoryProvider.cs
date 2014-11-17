using System.Collections;
using Taga.Core.IoC;

namespace TagKid.Core.Repository
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly Hashtable _repositories =new Hashtable();

        public TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository
        {
            var type = typeof(TRepository);
            if (_repositories.Contains(type))
                return (TRepository)_repositories[type];

            var repository = ServiceProvider.Provider.GetOrCreate<TRepository>();
            _repositories.Add(type, repository);
            return repository;
        }
    }
}
