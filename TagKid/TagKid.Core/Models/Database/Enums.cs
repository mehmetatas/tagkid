namespace TagKid.Core.Models.Database
{
    public enum UserStatus
    {
        Active,
        Passive,
        AwaitingActivation,
        Banned,
        Deleted
    }

    public enum LoginType
    {
        Email,
        Facebook,
        Cookie
    }

    public enum LoginResult
    {
        Successful,
        InvalidUsername,
        InvalidEmail,
        InvalidPassword,
        InvalidFacebookToken,
        InvalidCookieToken,
        ExpiredCookieToken,
        SystemError
    }

    public enum UserType
    {
        User,
        Admin,
        Moderator
    }

    public enum CategoryStatus
    {
        Active,
        Passive
    }

    public enum TagStatus
    {
        Active,
        Passive
    }

    public enum PostStatus
    {
        Active,
        Draft,
        Passive
    }

    public enum PostType
    {
        Text,
        Quote,
        Image,
        Audio,
        Video,
        Link,
        Retag
    }

    public enum CommentStatus
    {
        Deleted,
        Active
    }

    public enum AccessLevel
    {
        Public,
        Protected,
        Private
    }

    public enum ConfirmationCodeStatus
    {
        AwaitingConfirmation,
        Cancelled,
        Expired
    }

    public enum ConfirmationReason
    {
        NewUser,
        PasswordRecovery,
        AccountReactivation
    }

    public enum NotificationType
    {
        Like,
        Retag,
        NewFollower,
        Comment,
        PrivateMessage
    }

    public enum NotificationStatus
    {
        Unread,
        Read,
        Deleted
    }

    public enum PrivateMessageStatus
    {
        Unread,
        Read,
        Deleted
    }

    public enum TokenType
    {
        Auth,
        Request
    }
}