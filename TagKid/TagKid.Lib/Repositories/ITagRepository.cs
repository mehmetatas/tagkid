using System.Collections.Generic;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.Repositories
{
    public interface ITagRepository : ITagKidRepository
    {
        IEnumerable<Tag> GetAll();

        IPage<Tag> Search(string name, int pageIndex, int pageSize);

        IList<PostTagView> GetPostTags(params long[] postId);

        IDictionary<Tag, int> GetUserTagCounts(long userId);

        IDictionary<Tag, int> GetCategoryTagCounts(long userId);

        void Save(Tag tag);
    }
}