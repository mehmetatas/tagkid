﻿using Taga.Core.IoC;
using Taga.Core.Rest;
using TagKid.Core.Models.DTO.Messages.Auth;
using TagKid.Core.Models.DTO.Messages.Post;
using TagKid.Core.Models.DTO.Messages.User;
using TagKid.Core.Service;
using TagKid.Core.Service.Interceptors;
using IServiceProvider = Taga.Core.IoC.IServiceProvider;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class RestBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            var cfg = ServiceConfig.Builder();

            BuildAuthService(cfg);
            BuildUserService(cfg);
            BuildPostService(cfg);

            cfg.Build();

            prov.RegisterTransient<IActionInterceptor, ActionInterceptor>();
            prov.RegisterSingleton<IApiCallHandler>(new DefaultApiCallHandler());
        }

        private void BuildAuthService(ControllerConfigurator cfg)
        {
            cfg.ControllerFor<IAuthService>("auth")
                .ActionFor(s => s.SignUpWithEmail(default(SignUpWithEmailRequest)), "signUpWithEmail")
                .ActionFor(s => s.SignInWithPassword(default(SignInWithPasswordRequest)), "signInWithPassword")
                .ActionFor(s => s.SignInWithToken(default(SignInWithTokenRequest)), "signInWithToken")
                .ActionFor(s => s.SignOut(), "signOut")
                .ActionFor(s => s.ActivateAccount(default(ActivateAccountRequest)), "activateAccount", HttpMethodType.Get);
        }

        private void BuildUserService(ControllerConfigurator cfg)
        {
            cfg.ControllerFor<IUserService>("user")
                .ActionFor(s => s.GetProfile(default(GetProfileRequest)), "profile", HttpMethodType.Get);
        }

        private void BuildPostService(ControllerConfigurator cfg)
        {
            cfg.ControllerFor<IPostService>("post")
                .ActionFor(s => s.SaveAsDraft(default(SaveAsDraftRequest)), "saveAsDraft")
                .ActionFor(s => s.Publish(default(PublishRequest)), "publish")
                .ActionFor(s => s.GetTimeline(default(GetTimelineRequest)), "timeline", HttpMethodType.Get)
                .ActionFor(s => s.GetCategories(default(GetCategoriesRequest)), "categories", HttpMethodType.Get)
                .ActionFor(s => s.CreateCategory(default(CreateCategoryRequest)), "createCategory")
                .ActionFor(s => s.GetComments(default(GetCommentsRequest)), "comments", HttpMethodType.Get)
                .ActionFor(s => s.LikeUnlike(default(LikeUnlikeRequest)), "like")
                .ActionFor(s => s.GetPosts(default(GetPostsRequest)), "posts", HttpMethodType.Get);
        }
    }
}