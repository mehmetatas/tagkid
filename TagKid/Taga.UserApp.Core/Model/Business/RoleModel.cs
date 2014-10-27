using System.Collections.Generic;
using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Core.Model.Business
{
    public class RoleModel : Role
    {
        public List<PermissionModel> Permissions { get; set; }
    }
}
