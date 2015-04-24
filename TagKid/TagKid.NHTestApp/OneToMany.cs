using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TagKid.NHTestApp
{
    public class OneToManyFetcher<TParent, TChild> : IFetcher<TParent>
        where TParent : class, IEntity, new()
        where TChild : class, new()
    {
        private readonly Func<IEnumerable<long>, Expression<Func<TChild, bool>>> _childFilter;
        private readonly Func<TParent, Func<TChild, bool>> _parentFilter;
        private readonly Action<TParent, List<TChild>> _setChildren;

        public OneToManyFetcher(
            Func<IEnumerable<long>, Expression<Func<TChild, bool>>> childFilter,
            Func<TParent, Func<TChild, bool>> parentFilter,
            Action<TParent, List<TChild>> setChildren)
        {
            _childFilter = childFilter;
            _parentFilter = parentFilter;
            _setChildren = setChildren;
        }

        public void Fetch(IRepository repo, IList<TParent> parents)
        {
            var parentIds = parents.Select(p => p.Id);

            var allChildren = repo.Select<TChild>()
                .Where(_childFilter(parentIds))
                .ToList();

            foreach (var parent in parents)
            {
                var children = allChildren.Where(_parentFilter(parent)).ToList();
                _setChildren(parent, children);
            }
        }
    }

    //public class PostLikeFetcher : IFetcher<Post>
    //{
    //    public void Fetch(IRepository repo, IList<Post> posts)
    //    {
    //        var postIds = posts.Select(p => p.Id);

    //        var likes = repo.Select<Like>()
    //            .Where(l => postIds.Contains(l.Post.Id))
    //            .ToList();

    //        foreach (var post in posts)
    //        {
    //            post.Likes = likes.Where(l => l.Post.Id == post.Id).ToList();
    //        }
    //    }
    //}
}
