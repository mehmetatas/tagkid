using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface ICategoryRepository : ITagKidRepository
    {
        int GetCategoryCount(long userId);

        Category[] GetCategories(long userId);

        Category GetById(long categoryId);

        void Save(Category category);
    }
}