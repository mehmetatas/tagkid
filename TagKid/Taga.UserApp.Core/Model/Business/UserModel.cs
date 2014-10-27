using System.Collections.Generic;
using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Core.Model.Business
{
    public class UserModel : User
    {
        public List<RoleModel> Roles { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
