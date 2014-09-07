using System;
using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using TagKid.Lib.Models.Filters;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class PostRepository : IPostRepository
    {
        public PostView GetById(long postId)
        {
            return Db.SqlRepository().FirstOrDefault<PostView>(Db.SqlBuilder()
                .SelectAllFrom<PostView>()
                .Where("id").EqualsParam(postId)
                .Build());
        }

        public IPage<PostView> GetForUserId(long userId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IPage<PostView> Search(PostFilter filter)
        {
            var sqlBuilder = Db.SqlBuilder();

            sqlBuilder.Append("select p.* from post_search_view p");

            if (filter.ByTag)
                sqlBuilder.Join("tag_posts tp", "tp.post_id", "p.id");

            var and = String.Empty;
            sqlBuilder.Where();

            if (filter.ByUser)
            {
                sqlBuilder.Equals("p.user_id", filter.UserId);
                and = " and ";
            }

            if (filter.ByCategory)
            {
                sqlBuilder.Append(and)
                    .Equals("p.category_id", filter.CategoryId);
                and = " and ";
            }

            if (filter.ByTitle)
            {
                sqlBuilder.Append(and)
                    .Contains("p.title", filter.Title);
                and = " and ";
            }

            if (filter.ByTag)
            {
                sqlBuilder.Append(and)
                    .In("tp.tag_id", filter.TagIds);
                and = " and ";
            }

            if (filter.ByPostAccessLevel)
            {
                sqlBuilder.Append(and)
                    .In("p.access_level", filter.PostAccessLevels);
                and = " and ";
            }

            if (filter.ByCategoryAccessLevel)
            {
                sqlBuilder.Append(and)
                    .In("p.category_access_level", filter.CategoryAccessLevels);
            }

            sqlBuilder.OrderBy("p.publish_date", true);

            return Db.SqlRepository().ExecuteQuery<PostView>(sqlBuilder.Build(), filter.PageIndex, filter.PageSize);
        }

        public void Save(Post post, params Tag[] tags)
        {
            Db.SqlRepository().Save(post);

            foreach (var tag in tags)
            {
                Db.SqlRepository().Save(new PostTag { PostId = post.Id, TagId = tag.Id });
                Db.SqlRepository().Save(new TagPost { PostId = post.Id, TagId = tag.Id });
            }
        }
    }
}
