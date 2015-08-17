namespace TagKid.Core.Models.Database
{
    public enum UserStatus
    {
        Active,
        Passive,
        AwaitingActivation,
        Banned
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
    
    public enum AccessLevel
    {
        Private,
        Public
    }

    public enum ConfirmationCodeStatus
    {
        AwaitingConfirmation,
        Confirmed,
        Expired
    }

    public enum ConfirmationReason
    {
        Registration,
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
        Read
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