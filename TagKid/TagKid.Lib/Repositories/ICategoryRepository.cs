using System.Collections.Generic;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetByUserId(long userId);

        Category GetById(long categoryId);

        void Save(Category category);
    }
}
