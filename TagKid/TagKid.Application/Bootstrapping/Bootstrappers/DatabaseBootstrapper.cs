﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Mapping;
using Taga.Core.Repository.Mapping.NamingConvention;
using Taga.Repository.NH;
using Taga.Repository.NH.SpCallBuilders;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.View;
using IServiceProvider = Taga.Core.IoC.IServiceProvider;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class DatabaseBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            InitTableMappings();

            var sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012
                        .ConnectionString(ConfigurationManager.ConnectionStrings["tagkid"].ConnectionString)
                        .ShowSql())
                .Mappings(mappingConfig => mappingConfig.FluentMappings.AddFromAssemblyOf<UserMap>())
                .BuildSessionFactory();

            prov.RegisterSingleton<INHSpCallBuilder,SqlServerSpCallBuilder>();
            prov.RegisterPerWebRequest<ITransactionalUnitOfWork, NHUnitOfWork>();
            prov.RegisterPerWebRequest<IRepository, NHRepository>();
            prov.RegisterPerWebRequest<ISqlRepository, NHRepository>();

            //prov.RegisterSingleton<IHybridDbProvider, TagKidHybridDbProvider>();
            //prov.RegisterPerWebRequest<IHybridAdapter, NHHybridAdapter>();
            //prov.RegisterPerWebRequest<IUnitOfWork, HybridUnitOfWork>();
            //prov.RegisterPerWebRequest<ITransactionalUnitOfWork, HybridUnitOfWork>();
            //prov.RegisterPerWebRequest<IRepository, HybridRepository>();
            prov.RegisterSingleton(sessionFactory);
        }

        private static void InitTableMappings()
        {
            var dbMapping = DatabaseMapping
                .For(DbSystem.SqlServer)
                .WithNamingConvention(new SqlServerNamingConvention(false))
                .Map<Category>()
                .Map<Comment>()
                .Map<ConfirmationCode>()
                .Map<Login>()
                .Map<Notification>()
                .Map<FollowCategory>(fc => fc.UserId, fc => fc.CategoryId)
                .Map<FollowUser>(fu => fu.FollowerUserId, fu => fu.FollowedUserId)
                .Map<PostLike>(pl => pl.PostId, pl => pl.UserId)
                .Map<Post>()
                .Map<PostTag>(pt => pt.PostId, pt => pt.TagId)
                .Map<TagPost>(tp => tp.TagId, tp => tp.PostId)
                .Map<PrivateMessage>()
                .Map<Tag>()
                .Map<Token>()
                .Map<User>().ToTable("[User]")
                .Map<PostInfo>(pi => pi.PostId);
                
            var prov = ServiceProvider.Provider.GetOrCreate<IMappingProvider>();
            prov.SetDatabaseMapping(dbMapping);
        }
    }
}