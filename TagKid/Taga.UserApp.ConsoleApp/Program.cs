using System;
using System.Configuration;
using System.Data.Entity;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Taga.Core.IoC;
using Taga.Core.Json;
using Taga.Core.Mapping;
using Taga.Core.Repository;
using Taga.IoC.Ninject;
using Taga.Json.Newtonsoft;
using Taga.Mapping.AutoMapper;
using Taga.Repository.EF;
using Taga.Repository.NH;
using Taga.UserApp.Core.Database;
using Taga.UserApp.Core.Model.Business;
using Taga.UserApp.Core.Model.Database;
using Taga.UserApp.Core.Model.Mapping.EFMappings;
using Taga.UserApp.Core.Repository;
using Taga.UserApp.Repository;
using IServiceProvider = Taga.Core.IoC.IServiceProvider;

namespace Taga.UserApp.ConsoleApp
{
    class Program
    {
        private static IServiceProvider _provider;

        static void Main(string[] args)
        {
            try
            {
                InitApp();

                InsertNewUser();

                Console.WriteLine("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        private static void InsertNewUser()
        {
            using (var db = Db.Transactional())
            {
                db.BeginTransaction();

                var repo = db.GetRepository<IUserRepository>();

                repo.Save(new UserModel
                {
                    Username = "taga",
                    Password = "1234"
                });

                db.Save();
            }
        }

        private static void InitApp()
        {
            _provider = ServiceProvider.Provider = new NinjectServiceProvider();

            _provider.Register<IMapper, AutoMapper>();
            _provider.Register<IJsonSerializer, NewtonsoftJsonSerializer>();

            // InitEF();
            // InitNHMySql();
            InitNHSqlServer();
            InitMappings();

            _provider.Register<IUserRepository, UserRepository>();
        }

        private static void InitNHMySql()
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString(ConfigurationManager.ConnectionStrings["user_app_mysql"].ConnectionString)
                        .ShowSql())
                .Mappings(mappingConfig => mappingConfig.FluentMappings.AddFromAssemblyOf<User>())
                .BuildSessionFactory();

            _provider.Register<IUnitOfWork, NHUnitOfWork>();
            _provider.Register<ITransactionalUnitOfWork, NHUnitOfWork>();
            _provider.Register<IRepository, NHRepository>();
            _provider.RegisterSingleton(sessionFactory);
        }

        private static void InitNHSqlServer()
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012
                        .ConnectionString(ConfigurationManager.ConnectionStrings["user_app_sqlserver"].ConnectionString)
                        .ShowSql())
                .Mappings(mappingConfig => mappingConfig.FluentMappings.AddFromAssemblyOf<User>())
                .BuildSessionFactory();

            _provider.Register<IUnitOfWork, NHUnitOfWork>();
            _provider.Register<ITransactionalUnitOfWork, NHUnitOfWork>();
            _provider.Register<IRepository, NHRepository>();
            _provider.RegisterSingleton(sessionFactory);
        }

        private static void InitEF()
        {
            _provider.Register<DbContext, UserAppContext>();
            _provider.Register<IUnitOfWork, EFUnitOfWork>();
            _provider.Register<ITransactionalUnitOfWork, EFUnitOfWork>();
            _provider.Register<IRepository, EFRepository>();
        }

        private static void InitMappings()
        {
            var mapper = ServiceProvider.Provider.GetOrCreate<IMapper>();

            mapper.Register<UserModel, User>();
        }
    }
}
