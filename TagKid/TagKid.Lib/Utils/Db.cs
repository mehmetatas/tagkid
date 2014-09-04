using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Linq;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Repositories;

namespace TagKid.Lib.Utils
{
    class Db
    {
        public static IUnitOfWork UnitOfWork()
        {
            return GetOrCreate<IUnitOfWork>();
        }

        public static ISqlRepository SqlRepository()
        {
            return GetOrCreate<ISqlRepository>();
        }

        public static ILinqRepository LinqRepository()
        {
            return GetOrCreate<ILinqRepository>();
        }
        
        public static ISqlBuilder SqlBuilder()
        {
            return GetOrCreate<ISqlBuilder>();
        }

        public static ISql Sql(string sql, params object[] parameters)
        {
            return SqlBuilder().Append(sql, parameters).Build();
        }

        public static IUserRepository UserRepository()
        {
            return GetOrCreate<IUserRepository>();
        }

        public static ICategoryRepository CategoryRepository()
        {
            return GetOrCreate<ICategoryRepository>();
        }

        public static ICommentRepository CommentRepository()
        {
            return GetOrCreate<ICommentRepository>();
        }

        public static IConfirmationCodeRepository ConfirmationCodeRepository()
        {
            return GetOrCreate<IConfirmationCodeRepository>();
        }

        public static ILoginRepository LoginRepository()
        {
            return GetOrCreate<ILoginRepository>();
        }

        public static INotificationRepository NotificationRepository()
        {
            return GetOrCreate<INotificationRepository>();
        }

        public static IPostRepository PostRepository()
        {
            return GetOrCreate<IPostRepository>();
        }

        public static IPrivateMessageRepository PrivateMessageRepository()
        {
            return GetOrCreate<IPrivateMessageRepository>();
        }

        public static ITagRepository TagRepository()
        {
            return GetOrCreate<ITagRepository>();
        }

        private static T GetOrCreate<T>()
        {
            return ServiceProvider.Provider.GetOrCreate<T>();
        }
    }
}
