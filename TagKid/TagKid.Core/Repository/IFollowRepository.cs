using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface IFollowRepository : ITagKidRepository
    {
        void Save(FollowUser followUser);

        void Delete(FollowUser followUser);
        
        int GetFollowerCountOfUser(long followedUserId);

        int GetFollowedCountOfUser(long followerUserId);
        
        IPage<User> GetFollowersOfUser(long userId, int pageIndex, int pageSize);
        
        IPage<User> GetFollowedOfUser(long userId, int pageIndex, int pageSize);
    }
}