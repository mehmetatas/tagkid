using Taga.Core.Repository;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ICategoryRepository
    {
        IPage<Category> GetByUserId(long userId, int pageIndex, int pageSize);

        Category GetById(long categoryId);

        void Save(Category category);
    }
}
