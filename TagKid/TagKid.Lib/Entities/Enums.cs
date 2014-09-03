
namespace TagKid.Lib.Entities
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
        Facebook
    }

    public enum LoginResult
    {
        Successful,
        InvalidUsername,
        InvalidEmail,
        InvalidPassword,
        InvalidFacebookToken,
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
        Active,
        Passive
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
}
