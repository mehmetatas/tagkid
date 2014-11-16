using System;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Filter;

namespace TagKid.Core.Repository
{
    public interface IPostRepository : ITagKidRepository
    {
        Post GetById(long postId);

        Post[] GetForUserId(long userId, int maxCount, long maxPostId = 0);

        IPage<Post> Search(PostFilter filter);

        void Save(Post post);

        void Save(PostLike postLike);

        void Delete(PostLike postLike);
        
        int GetLikeCount(long postId);
        
        User[] GetLikers(long postId, int maxCount, DateTime maxLikeDate);
        int GetPostCount(long userId);
    }
}