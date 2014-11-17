﻿using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using TagKid.Core.Domain;
using TagKid.Core.Service;
using TagKid.Core.Service.Interceptors;
using TagKid.Domain;
using TagKid.Service;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class ServiceBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register(typeof(IAuthService), Proxy.TypeOf<AuthService>(typeof(IAuthServiceCallHandler)));
            prov.Register<IPostService, PostService>();

            prov.Register<IAuthServiceCallHandler, AuthServiceCallHandler>();

            prov.Register<IDomainServiceProvider, DomainServiceProvider>();
            prov.Register<IAuthDomainService, AuthDomainService>();
        }
    }
}