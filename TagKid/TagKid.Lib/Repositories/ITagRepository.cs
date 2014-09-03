using System.Collections.Generic;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> Search(string name, int pageIndex, int pageSize);

        IEnumerable<Tag> GetPostTags(long postId);

        IDictionary<Tag, int> GetUserTagCounts(long userId);

        IDictionary<Tag, int> GetCategoryTagCounts(long userId); 

        void Save(Tag tag);
    }
}
