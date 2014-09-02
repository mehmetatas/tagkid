using Taga.Core.IoC;
using Taga.Core.Repository;

namespace TagKid.Lib.Utils
{
    class Db
    {
        public static IUnitOfWork UnitOfWork()
        {
            return ServiceProvider.Provider.GetOrCreate<IUnitOfWork>();
        }

        public static IRepository Repository()
        {
            return ServiceProvider.Provider.GetOrCreate<IRepository>();
        }

        public static ISqlBuilder SqlBuilder()
        {
            return ServiceProvider.Provider.GetOrCreate<ISqlBuilder>();
        }

        public static ISql Sql(string sql, params object[] parameters)
        {
            return SqlBuilder().Append(sql, parameters).Build();
        }
    }
}
