using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface IFollowRepository
    {
        void Save(FollowCategory followCategory);

        void Save(FollowUser followUser);

        void Delete(FollowCategory followCategory);

        void Delete(FollowUser followUser);
        
        int GetFollowerCountOfCategory(long catId);
        
        int GetFollowerCountOfUser(long userId);
        
        int GetFollowingCountOfUser(long userId);
        
        IPage<User> GetFollowersOfCategory(long catId, int pageIndex, int pageSize);
        
        IPage<User> GetFollowersOfUser(long userId, int pageIndex, int pageSize);
        
        IPage<User> GetFollowingsOfUser(long userId, int pageIndex, int pageSize);
    }
}