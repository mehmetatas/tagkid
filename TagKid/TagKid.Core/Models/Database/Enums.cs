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
        Moderator,
        Anonymous
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

    public enum CommentStatus
    {
        Deleted,
        Active
    }

    public enum AccessLevel
    {
        Private,
        Public
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