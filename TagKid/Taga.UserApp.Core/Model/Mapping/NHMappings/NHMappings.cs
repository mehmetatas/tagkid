using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Core.Model.Mapping.NHMappings
{
    #region Tables

    public class UserMap : NHAutoMap<User>
    {
    }

    //public class UserMap : FluentNHibernate.Mapping.ClassMap<User>
    //{
    //    public UserMap()
    //    {
    //        Id(u => u.Id).GeneratedBy.Identity();
    //        //Map(u => u.Id);
    //        Map(u => u.Username);
    //        Map(u => u.Password);
    //        Table("Users");
    //    }
    //}

    public class RoleMap : NHAutoMap<Role>
    {
    }

    public class UserRoleMap : NHAutoMap<UserRole>
    {
        public UserRoleMap()
            : base(idProperty: "UserId")
        {
        }
    }

    public class PermissionMap : NHAutoMap<Permission>
    {
    }

    public class RolePermissionMap : NHAutoMap<RolePermission>
    {
        public RolePermissionMap()
            : base(idProperty: "RoleId")
        {
        }
    }

    public class CategoryMap : NHAutoMap<Category>
    {
    }

    public class PostMap : NHAutoMap<Post>
    {
    }

    public class TextPostMap : NHAutoMap<TextPost>
    {
        public TextPostMap()
            : base(idProperty: "PostId")
        {
        }
    }

    public class ImagePostMap : NHAutoMap<ImagePost>
    {
        public ImagePostMap()
            : base(idProperty: "PostId")
        {
        }
    }

    public class VideoPostMap : NHAutoMap<VideoPost>
    {
        public VideoPostMap()
            : base(idProperty: "PostId")
        {
        }
    }

    public class QuotePostMap : NHAutoMap<QuotePost>
    {
        public QuotePostMap()
            : base(idProperty: "PostId")
        {
        }
    }

    #endregion
}