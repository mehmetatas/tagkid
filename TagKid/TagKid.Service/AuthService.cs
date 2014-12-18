using System;
using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using TagKid.Core.Domain;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Auth;
using TagKid.Core.Service;

namespace TagKid.Service
{
    [Intercept]
    public class AuthService : IAuthService
    {
        private static IAuthDomainService DomainService
        {
            get { return ServiceProvider.Provider.GetOrCreate<IAuthDomainService>(); }
        }

        public virtual Response SignUpWithEmail(SignUpWithEmailRequest request)
        {
            DomainService.SignUpWithEmail(request.Email, request.Username, request.Password, request.Fullname);
            return Response.Success;
        }

        public virtual Response SignInWithPassword(SignInWithPasswordRequest request)
        {
            var user = DomainService.SignInWithPassword(request.EmailOrUsername, request.Password);
            return Response.Success.WithData(new
            {
                user.Username,
                user.Fullname,
                ProfileImageUrl = "/img/a2.jpg"
            });
        }

        public virtual Response ActivateAccount(ActivateAccountRequest request)
        {
            var user = DomainService.ActivateAccount(request.ConfirmationCodeId, request.ConfirmationCode);
            return Response.Success.WithData(new
            {
                user.Username,
                user.Fullname,
                ProfileImageUrl = "/img/a2.jpg"
            });
        }


        public virtual Response SignInWithToken(SignInWithTokenRequest request)
        {
            var user = DomainService.SignInWithToken(request.TokenId, new Guid(request.Token));
            return Response.Success.WithData(new
            {
                user.Username,
                user.Fullname,
                ProfileImageUrl = "/img/a2.jpg"
            });
        }

        public virtual Response SignOut()
        {
            DomainService.SignOut();
            return Response.Success;
        }
    }
}