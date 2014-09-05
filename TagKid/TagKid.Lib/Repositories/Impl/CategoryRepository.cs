using Taga.Core.Repository;
using Taga.Core.Repository.Linq;
using TagKid.Lib.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        public IPage<Category> GetByUserId(long userId, int pageIndex, int pageSize)
        {
            return Db.LinqRepository().Query<Category>(cat => cat.UserId == userId, pageIndex, pageSize);
        }

        public Category GetById(long categoryId)
        {
            return Db.LinqRepository().FirstOrDefault<Category>(cat => cat.Id == categoryId);
        }

        public void Save(Category category)
        {
            Db.LinqRepository().Save(category);
        }
    }
}
