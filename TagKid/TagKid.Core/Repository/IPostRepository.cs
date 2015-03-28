using System;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.View;
using TagKid.Core.Models.Filter;

namespace TagKid.Core.Repository
{
    public interface IPostRepository : ITagKidRepository
    {
        Post GetById(long postId);

        Post[] GetForUserId(long userId, int maxCount, long maxPostId = 0);

        PostInfo[] GetPostInfo(long userId, params long[] postIds);

        IPage<Post> Search(PostFilter filter);

        void Save(Post post);

        void Save(PostLike postLike);

        void Delete(PostLike postLike);

        int GetLikeCount(long postId);

        void Like(long postId, long userId);

        void Unlike(long postId, long userId);

        bool HasLiked(long postId, long userId);
        
        User[] GetLikers(long postId, int maxCount, DateTime maxLikeDate);

        int GetPublicPostCount(long userId);

        Post[] GetPostsOfUser(long userId, int maxCount, long maxPostId);
    }
}