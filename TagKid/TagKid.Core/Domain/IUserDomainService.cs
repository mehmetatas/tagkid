using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Domain;

namespace TagKid.Core.Domain
{
    public interface IUserDomainService : ITagKidDomainService
    {
        void DeactivateAccount();

        void SignOut();

        ProfileDO GetProfile(string username);

        IPage<Notification> GetAllNotifications(int pageIndex, int pageSize);

        IPage<Notification> GetUnreadNotifications(int pageIndex, int pageSize);

        void ReadNotification(long notificationId);

        void FollowUser(long userId);

        void UnfollowUser(long userId);

        void FollowCategory(long categoryId);

        void UnfollowCategory(long categoryId);

        IPage<User> GetFollowers(int pageIndex, int pageSize);

        IPage<User> GetFolloweds(int pageIndex, int pageSize);

        void SendPrivateMessage(long toUserId, string message);

        void ReadPrivateMessage(long messageId);

        void PublishPost(Post post);

        void DeletePost(Post post);

        void SavePostAsDraft(Post post);

        IPage<Post> GetDrafts();

        void LikePost(long postId);

        void UnlikePost(long postId);

        void SendComment(Comment comment);

        void DeleteComment(long commentId);
    }
}
