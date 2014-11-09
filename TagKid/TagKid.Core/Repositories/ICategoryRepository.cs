using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface ICategoryRepository : ITagKidRepository
    {
        Category[] GetCategoriesOfUser(long userId);

        Category GetById(long categoryId);

        void Save(Category category);
    }
}