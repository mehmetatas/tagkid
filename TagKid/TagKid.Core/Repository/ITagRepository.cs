using System.Collections.Generic;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface ITagRepository : ITagKidRepository
    {
        Tag[] GetAll();

        IPage<Tag> Search(string name, int pageIndex, int pageSize);

        IDictionary<Tag, int> GetUserTagCounts(long userId);

        void Save(Tag tag);
    }
}