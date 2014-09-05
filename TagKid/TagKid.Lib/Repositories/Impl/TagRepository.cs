using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using Taga.Core.Repository.Linq;
using TagKid.Lib.Entities;
using TagKid.Lib.Entities.Views;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class TagRepository : ITagRepository
    {
        public IEnumerable<Tag> GetAll()
        {
            return Db.SqlRepository().Select<Tag>(Db.Sql("select * from tags"));
        }

        public IPage<Tag> Search(string name, int pageIndex, int pageSize)
        {
            return Db.LinqRepository().Query<Tag>(t => t.Name.Contains(name), pageIndex, pageSize);
        }

        public IEnumerable<Tag> GetPostTags(long postId)
        {
            return Db.SqlRepository().Select<Tag>(
                Db.Sql("select * from tags wwhere id in (select tag_id from post_tags where post_id = @0)",
                    postId));
        }

        public IDictionary<Tag, int> GetUserTagCounts(long userId)
        {
            var userTags = Db.SqlRepository().Select<UserTagsView>(
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
            Db.LinqRepository().Save(tag);
        }
    }
}
