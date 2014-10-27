using System.Data.Entity;
using Taga.UserApp.Core.Model.Business;
using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Core.Model.Mapping.EFMappings
{
    public class UserAppContext : DbContext
    {
        static UserAppContext()
        {
            System.Data.Entity.Database.SetInitializer<UserAppContext>(null);
        }

        public UserAppContext()
            : base("user_app_sqlserver")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<UserModel>();

            modelBuilder
                .Entity<User>().HasKey(u => u.Id).ToTable(GetTableName<User>())
                .Property(u => u.Id).HasColumnName("Id");
        }

        private static string GetTableName<T>()
        {
            var type = typeof(T);

            var tableName = type.Name;

            if (tableName.EndsWith("y"))
                tableName = tableName.Substring(0, tableName.Length - 1) + "ies";
            else
                tableName += "s";

            return tableName;
        }
    }

}
