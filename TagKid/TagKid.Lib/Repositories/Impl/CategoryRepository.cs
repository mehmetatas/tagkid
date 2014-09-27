using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        public IPage<Category> GetByUserId(long userId, int pageIndex, int pageSize)
        {
            return Db.SqlRepository().ExecuteQuery<Category>(Db.SqlBuilder()
                .SelectAllFrom<Category>()
                .Where("user_id").EqualsParam(userId)
                .Build(), pageIndex, pageSize);
        }

        public Category GetById(long categoryId)
        {
            return Db.SqlRepository().FirstOrDefault<Category>(cat => cat.Id, categoryId);
        }

        public void Save(Category category)
        {
            Db.SqlRepository().Save(category);
        }
    }
}
