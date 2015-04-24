using TagKid.Framework.Repository.Impl;

namespace TagKid.Framework.Models.Database.Mappings
{
    public class UserMap : NHAutoClassMap<User>
    {
    }

    public class TagMap : NHAutoClassMap<Tag>
    {
    }

    public class TokenMap : NHAutoClassMap<Token>
    {
    }

    public class PostMap : NHAutoClassMap<Post>
    {
    }

    public class PostLikeMap : NHAutoClassMap<Like>
    {
    }

    public class PostTagMap : NHAutoClassMap<PostTag>
    {
    }

    public class PrivateMessageMap : NHAutoClassMap<PrivateMessage>
    {
    }

    public class NotificationMap : NHAutoClassMap<Notification>
    {
    }

    public class LoginMap : NHAutoClassMap<Login>
    {
    }

    public class CommentMap : NHAutoClassMap<Comment>
    {
    }

    public class ConfirmationCodeMap : NHAutoClassMap<ConfirmationCode>
    {
    }

    public class FollowUserMap : NHAutoClassMap<FollowUser>
    {
    }
}