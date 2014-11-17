using System.Collections;
using Taga.Core.IoC;

namespace TagKid.Core.Domain
{
    public class DomainServiceProvider : IDomainServiceProvider
    {
        private readonly Hashtable _repositories;

        public DomainServiceProvider()
        {
            _repositories = new Hashtable();
        }

        public TService GetService<TService>() where TService : ITagKidDomainService
        {
            var type = typeof(TService);
            if (_repositories.Contains(type))
                return (TService)_repositories[type];

            var repository = ServiceProvider.Provider.GetOrCreate<TService>();
            _repositories.Add(type, repository);
            return repository;
        }
    }
}
