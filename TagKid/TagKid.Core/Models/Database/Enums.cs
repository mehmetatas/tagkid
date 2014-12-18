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

    public enum EditorType
    {
        TagKid,
        Cke,
        MarkDown
    }

    public enum UserType
    {
        User,
        Admin,
        Moderator
    }

    public enum CategoryStatus
    {
        Passive,
        Active
    }

    public enum TagStatus
    {
        Passive,
        Active
    }

    public enum PostStatus
    {
        Published,
        Draft,
        Deleted,
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
        Confirmed,
        Failed,
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
        Read
    }

    public enum TokenType
    {
        Auth,
        Request
    }
}