using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface IFollowRepository : ITagKidRepository
    {
        void Save(FollowCategory followCategory);

        void Save(FollowUser followUser);

        void Delete(FollowCategory followCategory);

        void Delete(FollowUser followUser);
        
        int GetFollowerCountOfCategory(long catId);

        int GetFollowerCountOfUser(long followedUserId);

        int GetFollowedCountOfUser(long followerUserId);
        
        IPage<User> GetFollowersOfCategory(long catId, int pageIndex, int pageSize);
        
        IPage<User> GetFollowersOfUser(long userId, int pageIndex, int pageSize);
        
        IPage<User> GetFollowedOfUser(long userId, int pageIndex, int pageSize);
    }
}