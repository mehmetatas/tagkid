using Taga.Repository.NH;
using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Core.Database.NH
{
    #region Tables

    public class UserMap : TagaClassMap<User>
    {
    }

    public class RoleMap : TagaClassMap<Role>
    {
    }

    public class PermissionMap : TagaClassMap<Permission>
    {
    }

    public class UserRoleMap : TagaClassMap<UserRole>
    {
    }

    public class RolePermissionMap : TagaClassMap<RolePermission>
    {
    }

    public class CategoryMap : TagaClassMap<Category>
    {
    }

    public class PostMap : TagaClassMap<Post>
    {
    }

    public class TextPostMap : TagaClassMap<TextPost>
    {
    }

    public class ImagePostMap : TagaClassMap<ImagePost>
    {
    }

    public class VideoPostMap : TagaClassMap<VideoPost>
    {
    }

    public class QuotePostMap : TagaClassMap<QuotePost>
    {
    }

    #endregion
}