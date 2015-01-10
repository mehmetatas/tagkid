using Taga.Repository.NH;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.View;

namespace TagKid.Application.Bootstrapping
{
    public class UserMap : TagaClassMap<User>
    {
    }

    public class TagMap : TagaClassMap<Tag>
    {
    }

    public class TokenMap : TagaClassMap<Token>
    {
    }

    public class PostMap : TagaClassMap<Post>
    {
    }

    public class PostLikeMap : TagaClassMap<PostLike>
    {
    }

    public class PostTagMap : TagaClassMap<PostTag>
    {
    }

    public class TagPostMap : TagaClassMap<TagPost>
    {
    }

    public class PrivateMessageMap : TagaClassMap<PrivateMessage>
    {
    }

    public class NotificationMap : TagaClassMap<Notification>
    {
    }

    public class LoginMap : TagaClassMap<Login>
    {
    }

    public class CommentMap : TagaClassMap<Comment>
    {
    }

    public class CategoryMap : TagaClassMap<Category>
    {
    }

    public class ConfirmationCodeMap : TagaClassMap<ConfirmationCode>
    {
    }

    public class FollowUserMap : TagaClassMap<FollowUser>
    {
    }

    public class FollowCategoryMap : TagaClassMap<FollowCategory>
    {
    }

    public class PostInfoMap : TagaClassMap<PostInfo>
    {
    }
}