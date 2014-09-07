using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class TagRepository : ITagRepository
    {
        public IEnumerable<Tag> GetAll()
        {
            return Db.SqlRepository().List<Tag>(Db.SqlBuilder()
                .SelectAllFrom("tags")
                .OrderBy("name")
                .Build());
        }

        public IPage<Tag> Search(string name, int pageIndex, int pageSize)
        {
            return Db.SqlRepository().ExecuteQuery<Tag>(Db.SqlBuilder()
                .SelectAllFrom<Tag>()
                .Where("name").Contains(name)
                .Build(),
                pageIndex, pageSize);
        }

        public IEnumerable<Tag> GetPostTags(long postId)
        {
            return Db.SqlRepository().List<Tag>(Db.SqlBuilder()
                .SelectAllFrom("tags").Where("id")
                .Append("in (select tag_id from post_tags where post_id = @0)", postId)
                .Build());
        }

        public IDictionary<Tag, int> GetUserTagCounts(long userId)
        {
            var userTags = Db.SqlRepository().List<UserTagsView>(
                Db.Sql(@"select tv.*, t.name from user_tags_view tv
                    join tags t on t.id = tag_id
                    where user_id = @0
                    order by tag_count desc",
                    userId));

            return userTags.ToDictionary(k => k as Tag, v => v.TagCount);
        }

        public IDictionary<Tag, int> GetCategoryTagCounts(long userId)
        {
            throw new NotImplementedException();
        }

        public void Save(Tag tag)
        {
            Db.SqlRepository().Save(tag);
        }
    }
}
