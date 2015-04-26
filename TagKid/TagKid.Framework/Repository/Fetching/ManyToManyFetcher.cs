using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TagKid.Framework.Repository.Fetching
{
    public class ManyToManyFetcher<TParent, TChild, TAssoc> : IFetcher<TParent>
        where TParent : class, IEntity, new()
        where TChild : class, IEntity, new()
        where TAssoc : class, new()
    {
        private readonly Func<IEnumerable<long>, Expression<Func<TAssoc, bool>>> _assocFilter;
        private readonly Func<Expression<Func<TAssoc, ManyToManyItem<TChild>>>> _assocSelect;
        private readonly Action<TParent, List<TChild>> _setChildren;

        public ManyToManyFetcher(
            Func<IEnumerable<long>, Expression<Func<TAssoc, bool>>> childFilter,
            Func<Expression<Func<TAssoc, ManyToManyItem<TChild>>>> assocSelect,
            Action<TParent, List<TChild>> setChildren)
        {
            _assocFilter = childFilter;
            _assocSelect = assocSelect;
            _setChildren = setChildren;
        }

        public void Fetch(IRepository repo, IList<TParent> posts)
        {
            var postIds = posts.Select(p => p.Id);

            var postTags = repo.Select<TAssoc>()
                .Where(_assocFilter(postIds))
                .Select(_assocSelect())
                .ToList();

            foreach (var post in posts)
            {
                var tags = postTags.Where(r => r.ParentId == post.Id).Select(r => r.Child).ToList();
                _setChildren(post, tags);
            }
        }
    }

    public class ManyToManyItem<TChild>
    {
        public long ParentId { get; set; }
        public TChild Child { get; set; }
    }

    //public class PostTagFetcher : IFetcher<Post>
    //{
    //    public void Fetch(IRepository repo, IList<Post> posts)
    //    {
    //        var postIds = posts.Select(p => p.Id);

    //        var postTags = repo.Select<PostTag>()
    //            .Where(pt => postIds.Contains(pt.Post.Id))
    //            .Select(pt =>
    //            new ManyToManyItem<Tag>
    //            {
    //                ParentId = pt.Post.Id,
    //                Child = pt.Tag
    //            })
    //            .ToList();

    //        foreach (var post in posts)
    //        {
    //            post.Tags = postTags.Where(item => item.ParentId == post.Id).Select(item => item.Child).ToList();
    //        }
    //    }
    //}
}
