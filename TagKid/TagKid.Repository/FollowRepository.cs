using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IRepository _repository;

        public FollowRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void Save(FollowUser followUser)
        {
            _repository.Insert(followUser);
        }

        public void Delete(FollowUser followUser)
        {
            _repository.Delete(followUser);
        }

        public int GetFollowerCountOfUser(long followedUserId)
        {
            return _repository.Select<FollowUser>()
                .Count(fu => fu.FollowedUserId == followedUserId);
        }

        public int GetFollowedCountOfUser(long followerUserId)
        {
            return _repository.Select<FollowUser>()
                .Count(fu => fu.FollowerUserId == followerUserId);
        }
        
        public IPage<User> GetFollowersOfUser(long followedUserId, int pageIndex, int pageSize)
        {
            var users = _repository.Select<User>();
            var followUsers = _repository.Select<FollowUser>();

            var query = (from user in users
                         from followUser in followUsers
                         where followUser.FollowedUserId == followedUserId &&
                               followUser.FollowerUserId == user.Id
                         orderby user.Username
                         select user);

            return query.Page(pageIndex, pageSize);
        }

        public IPage<User> GetFollowedOfUser(long followerUserId, int pageIndex, int pageSize)
        {
            var users = _repository.Select<User>();
            var followUsers = _repository.Select<FollowUser>();

            var query = (from user in users
                         from followUser in followUsers
                         where followUser.FollowerUserId == followerUserId &&
                               followUser.FollowedUserId == user.Id
                         orderby user.Username
                         select user);

            return query.Page(pageIndex, pageSize);
        }

        public void Flush()
        {
            _repository.Flush();
        }
    }
}
