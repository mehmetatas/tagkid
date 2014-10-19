using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository _repository;

        public CategoryRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IPage<Category> GetByUserId(long userId, int pageIndex, int pageSize)
        {
            return _repository.Query<Category>()
                .Where(c => c.UserId == userId)
                .Page(pageIndex, pageSize);
        }

        public Category GetById(long categoryId)
        {
            return _repository.Query<Category>().FirstOrDefault(cat => cat.Id == categoryId);
        }

        public void Save(Category category)
        {
            _repository.Save(category);
        }
    }
}
