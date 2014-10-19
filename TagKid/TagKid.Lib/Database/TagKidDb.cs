using System.Collections;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Lib.Repositories;

namespace TagKid.Lib.Database
{
    public class TagKidDb : IUnitOfWork, IRepositoryProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly Hashtable _repositories;

        public TagKidDb()
        {
            _unitOfWork = ServiceProvider.Provider.GetOrCreate<IUnitOfWork>();
            _repositories = new Hashtable();
        }

        public virtual TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository
        {
            var type = typeof (TRepository);
            if (_repositories.Contains(type))
                return (TRepository)_repositories[type];

            var repository = ServiceProvider.Provider.GetOrCreate<TRepository>();
            _repositories.Add(type, repository);
            return repository;
        }

        public virtual void Save()
        {
            _unitOfWork.Save();
        }

        public virtual void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
