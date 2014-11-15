using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;

namespace TagKid.Repository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IRepository _repository;

        public FollowRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void Save(FollowCategory followCategory)
        {
            _repository.Insert(followCategory);
        }

        public void Save(FollowUser followUser)
        {
            _repository.Insert(followUser);
        }

        public void Delete(FollowCategory followCategory)
        {
            _repository.Delete(followCategory);
        }

        public void Delete(FollowUser followUser)
        {
            _repository.Delete(followUser);
        }

        public int GetFollowerCountOfCategory(long catId)
        {
            return _repository.Select<FollowCategory>()
                .Count(fc => fc.CategoryId == catId);
        }

        public int GetFollowerCountOfUser(long userId)
        {
            return _repository.Select<FollowUser>()
                .Count(fu => fu.UserId2 == userId);
        }

        public int GetFollowingCountOfUser(long userId)
        {
            return _repository.Select<FollowUser>()
                .Count(fu => fu.UserId1 == userId);
        }

        public IPage<User> GetFollowersOfCategory(long catId, int pageIndex, int pageSize)
        {
            var users = _repository.Select<User>();
            var followCategories = _repository.Select<FollowCategory>();

            var query = (from user in users
                         from followCategory in followCategories
                         where followCategory.CategoryId == catId &&
                               followCategory.UserId == user.Id
                         orderby user.Username
                         select user);

            return query.Page(pageIndex, pageSize);
        }

        public IPage<User> GetFollowersOfUser(long userId, int pageIndex, int pageSize)
        {
            var users = _repository.Select<User>();
            var followUsers = _repository.Select<FollowUser>();

            var query = (from user in users
                         from followUser in followUsers
                         where followUser.UserId2 == userId &&
                               followUser.UserId1 == user.Id
                         orderby user.Username
                         select user);

            return query.Page(pageIndex, pageSize);
        }

        public IPage<User> GetFollowingsOfUser(long userId, int pageIndex, int pageSize)
        {
            var users = _repository.Select<User>();
            var followUsers = _repository.Select<FollowUser>();

            var query = (from user in users
                         from followUser in followUsers
                         where followUser.UserId1 == userId &&
                               followUser.UserId2 == user.Id
                         orderby user.Username
                         select user);

            return query.Page(pageIndex, pageSize);
        }
    }
}
