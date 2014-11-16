using System;
using Taga.Core.Repository;
using TagKid.Core.Domain;
using TagKid.Core.Models;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Domain;
using TagKid.Core.Repository;

namespace TagKid.Domain
{
    public class DomainUser : IDomainUser
    {
        private readonly User _user;
        private readonly IRepositoryProvider _prov;

        public DomainUser(IRepositoryProvider prov)
            : this(RequestContext.Current.User, prov)
        {
        }

        public DomainUser(User user, IRepositoryProvider prov)
        {
            _user = user;
            _prov = prov;
        }

        public User User
        {
            get { return _user; }
        }

        private IFollowRepository FollowRepository
        {
            get { return _prov.GetRepository<IFollowRepository>(); }
        }

        private IPostRepository PostRepository
        {
            get { return _prov.GetRepository<IPostRepository>(); }
        }

        private ICommentRepository CommentRepository
        {
            get { return _prov.GetRepository<ICommentRepository>(); }
        }

        private INotificationRepository NotificationRepository
        {
            get { return _prov.GetRepository<INotificationRepository>(); }
        }

        private ICategoryRepository CategoryRepository
        {
            get { return _prov.GetRepository<ICategoryRepository>(); }
        }

        private IPrivateMessageRepository PrivateMessageRepository
        {
            get { return _prov.GetRepository<IPrivateMessageRepository>(); }
        }

        private IUserRepository UserRepository
        {
            get { return _prov.GetRepository<IUserRepository>(); }
        }

        private ITokenRepository TokenRepository
        {
            get { return _prov.GetRepository<ITokenRepository>(); }
        }

        public virtual void DeactivateAccount()
        {
            _user.Status = UserStatus.Passive;
            UserRepository.Save(_user);
        }

        public virtual void SignOut()
        {
            throw new NotImplementedException();
        }

        public virtual Profile GetProfile()
        {
            return new Profile(_user,
                FollowRepository.GetFollowerCountOfUser(_user.Id),
                FollowRepository.GetFollowedCountOfUser(_user.Id),
                PostRepository.GetPostCount(_user.Id),
                CategoryRepository.GetCategoryCount(_user.Id));
        }

        public virtual Category CreateCategory(string name, string description, AccessLevel accessLevel)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        public virtual Category[] GetCategories()
        {
            throw new NotImplementedException();
        }

        public virtual IPage<Notification> GetAllNotifications(int pageIndex, int pageSize)
        {
            return NotificationRepository.GetNotifications(_user.Id, false, pageIndex, pageSize);
        }

        public virtual IPage<Notification> GetUnreadNotifications(int pageIndex, int pageSize)
        {
            return NotificationRepository.GetNotifications(_user.Id, true, pageIndex, pageSize);
        }

        public virtual void ReadNotification(long notificationId)
        {
            var notification = NotificationRepository.GetNotification(notificationId);

            if (notification == null)
            {
                throw new Exception("");
            }

            if (notification.ToUserId != _user.Id)
            {
                throw new Exception();
            }

            notification.Status = NotificationStatus.Read;
            NotificationRepository.Save(notification);
        }

        public virtual void FollowUser(long userId)
        {
            FollowRepository.Save(new FollowUser
            {
                FollowerUserId = _user.Id,
                FollowedUserId = userId
            });
        }

        public virtual void UnfollowUser(long userId)
        {
            FollowRepository.Delete(new FollowUser
            {
                FollowerUserId = _user.Id,
                FollowedUserId = userId
            });
        }

        public virtual void FollowCategory(long categoryId)
        {
            FollowRepository.Save(new FollowCategory
            {
                UserId = _user.Id,
                CategoryId = categoryId
            });
        }

        public virtual void UnfollowCategory(long categoryId)
        {
            FollowRepository.Delete(new FollowCategory
            {
                UserId = _user.Id,
                CategoryId = categoryId
            });
        }

        public virtual IPage<User> GetFollowers(int pageIndex, int pageSize)
        {
            return FollowRepository.GetFollowersOfUser(_user.Id, pageIndex, pageSize);
        }

        public virtual IPage<User> GetFolloweds(int pageIndex, int pageSize)
        {
            return FollowRepository.GetFollowedOfUser(_user.Id, pageIndex, pageSize);
        }

        public virtual void SendPrivateMessage(long toUserId, string message)
        {
            PrivateMessageRepository.Save(new PrivateMessage
            {
                FromUserId = _user.Id,
                ToUserId = toUserId,
                Message = message,
                MessageDate = DateTime.Now,
                Status = PrivateMessageStatus.Unread
            });
        }

        public virtual void ReadPrivateMessage(long messageId)
        {
            var message = PrivateMessageRepository.GetMessage(messageId);

            if (message == null)
            {
                throw new Exception();
            }

            if (message.ToUserId != _user.Id)
            {
                throw new Exception();
            }

            message.Status = PrivateMessageStatus.Read;

            PrivateMessageRepository.Save(message);
        }

        public virtual void PublishPost(Post post)
        {
            throw new NotImplementedException();
        }

        public virtual void DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        public virtual void SavePostAsDraft(Post post)
        {
            throw new NotImplementedException();
        }

        public virtual void LikePost(long postId)
        {
            PostRepository.Save(new PostLike
            {
                UserId = _user.Id,
                PostId = postId
            });
        }

        public virtual void UnlikePost(long postId)
        {
            PostRepository.Delete(new PostLike
            {
                UserId = _user.Id,
                PostId = postId
            });
        }

        public virtual void SendComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteComment(long commentId)
        {
            var comment = CommentRepository.Get(commentId);

            if (comment == null)
            {
                throw new Exception();
            }

            if (comment.UserId != _user.Id)
            {
                var post = _prov.GetRepository<IPostRepository>()
                    .GetById(comment.PostId);

                if (post == null)
                {
                    throw new Exception();
                }

                if (post.UserId != _user.Id)
                {
                    throw new Exception();   
                }
            }

            comment.Status = CommentStatus.Deleted;

            CommentRepository.Save(comment);
        }
    }
}
