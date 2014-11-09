using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;

namespace TagKid.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository _repository;

        public CategoryRepository(IRepository repository)
        {
            _repository = repository;
        }
        
        public Category[] GetCategoriesOfUser(long userId)
        {
            return _repository.Select<Category>()
                .Where(c => c.UserId == userId)
                .ToArray();
        }

        public Category GetById(long categoryId)
        {
            return _repository.Select<Category>().FirstOrDefault(cat => cat.Id == categoryId);
        }

        public void Save(Category category)
        {
            _repository.Save(category);
        }
    }
}