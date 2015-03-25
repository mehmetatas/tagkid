using System;
using Taga.Core.IoC;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Domain;
using TagKid.Core.Repository;

namespace TagKid.Domain
{
    public class UserDomainService : IUserDomainService
    {
        private static IUserRepository UserRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<IUserRepository>(); }
        }

        private static ITagRepository TagRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<ITagRepository>(); }
        }

        private static IPostRepository PostRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<IPostRepository>(); }
        }

        public void DeactivateAccount()
        {
            throw new NotImplementedException();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        public ProfileDO GetProfile(string username)
        {
            var user = UserRepository.GetByUsername(username);
            
            if (user == null || user.Status != UserStatus.Active)
            {
                throw Errors.U_ProfileError.ToException();
            }

            return new ProfileDO(user);
        }

        public Taga.Core.Repository.IPage<Notification> GetAllNotifications(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Taga.Core.Repository.IPage<Notification> GetUnreadNotifications(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void ReadNotification(long notificationId)
        {
            throw new NotImplementedException();
        }

        public void FollowUser(long userId)
        {
            throw new NotImplementedException();
        }

        public void UnfollowUser(long userId)
        {
            throw new NotImplementedException();
        }

        public void FollowCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        public void UnfollowCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        public Taga.Core.Repository.IPage<Core.Models.Database.User> GetFollowers(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Taga.Core.Repository.IPage<Core.Models.Database.User> GetFolloweds(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void SendPrivateMessage(long toUserId, string message)
        {
            throw new NotImplementedException();
        }

        public void ReadPrivateMessage(long messageId)
        {
            throw new NotImplementedException();
        }

        public void PublishPost(Core.Models.Database.Post post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Core.Models.Database.Post post)
        {
            throw new NotImplementedException();
        }

        public void SavePostAsDraft(Core.Models.Database.Post post)
        {
            throw new NotImplementedException();
        }

        public Taga.Core.Repository.IPage<Core.Models.Database.Post> GetDrafts()
        {
            throw new NotImplementedException();
        }

        public void LikePost(long postId)
        {
            throw new NotImplementedException();
        }

        public void UnlikePost(long postId)
        {
            throw new NotImplementedException();
        }

        public void SendComment(Core.Models.Database.Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(long commentId)
        {
            throw new NotImplementedException();
        }
    }
}
