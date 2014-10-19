using System.Collections.Generic;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ITagRepository : ITagKidRepository
    {
        IEnumerable<Tag> GetAll();

        IPage<Tag> Search(string name, int pageIndex, int pageSize);

        IEnumerable<Tag> GetPostTags(long postId);

        IDictionary<Tag, int> GetUserTagCounts(long userId);

        IDictionary<Tag, int> GetCategoryTagCounts(long userId); 

        void Save(Tag tag);
    }
}
