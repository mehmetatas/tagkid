using System.Data.Entity;
using Taga.Repository.EF;
using Taga.UserApp.Core.Model.Business;

namespace Taga.UserApp.Core.Database.EF
{
    public class UserAppContext : TagaDbContext
    {
        static UserAppContext()
        {
            System.Data.Entity.Database.SetInitializer<UserAppContext>(null);
        }

        protected UserAppContext(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public UserAppContext()
            : base("user_app_sqlserver")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<UserModel>();
            modelBuilder.Ignore<RoleModel>();
            modelBuilder.Ignore<PermissionModel>();
            modelBuilder.Ignore<CategoryModel>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
