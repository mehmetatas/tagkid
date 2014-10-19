using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.NHibernate.Mappings
{
    #region Tables

    public class UserMap : NHAutoMap<User>
    {
    }

    public class TagMap : NHAutoMap<Tag>
    {
    }

    public class TokenMap : NHAutoMap<Token>
    {
    }

    public class PostMap : NHAutoMap<Post>
    {
    }

    public class PostTagMap : NHAutoMap<PostTag>
    {
        public PostTagMap()
            : base(idProperty: "PostId")
        {

        }
    }

    public class TagPostMap : NHAutoMap<TagPost>
    {
        public TagPostMap()
            : base(idProperty: "TagId")
        {

        }
    }

    public class PrivateMessageMap : NHAutoMap<PrivateMessage>
    {
    }

    public class NotificationMap : NHAutoMap<Notification>
    {
    }

    public class LoginMap : NHAutoMap<Login>
    {
    }

    public class CommentMap : NHAutoMap<Comment>
    {
    }

    public class CategoryMap : NHAutoMap<Category>
    {
    }

    public class ConfirmationCodeMap : NHAutoMap<ConfirmationCode>
    {
    }

    #endregion

    #region Views

    public class PostViewMap : NHAutoMap<PostView>
    {
        public PostViewMap()
            : base("post_search_view")
        {
        }
    }

    #endregion
}
