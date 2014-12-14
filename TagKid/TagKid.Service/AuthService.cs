using System;
using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using Taga.Core.Mapping;
using TagKid.Core.Domain;
using TagKid.Core.Models.DTO.Messages.Auth;
using TagKid.Core.Service;

namespace TagKid.Service
{
    [Intercept]
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;

        public AuthService(IMapper mapper)
        {
            _mapper = mapper;
        }

        private static IAuthDomainService DomainService
        {
            get { return ServiceProvider.Provider.GetOrCreate<IAuthDomainService>(); }
        }

        public virtual SignUpWithEmailResponse SignUpWithEmail(SignUpWithEmailRequest request)
        {
            DomainService.SignUpWithEmail(request.Email, request.Username, request.Password, request.Fullname);
            return new SignUpWithEmailResponse();
        }

        public virtual SignInWithPasswordResponse SignInWithPassword(SignInWithPasswordRequest request)
        {
            var user = DomainService.SignInWithPassword(request.EmailOrUsername, request.Password);
            return new SignInWithPasswordResponse
            {
                Username = user.Username,
                Fullname = user.Fullname,
                ProfileImageUrl = "/img/a2.jpg"
            };
        }

        public virtual ActivateAccountResponse ActivateAccount(ActivateAccountRequest request)
        {
         var user =   DomainService.ActivateAccount(request.ConfirmationCodeId, request.ConfirmationCode);
            return new ActivateAccountResponse
            {
                Username = user.Username,
                Fullname = user.Fullname,
                ProfileImageUrl = "/img/a2.jpg"
            };
        }


        public virtual SignInWithTokenResponse SignInWithToken(SignInWithTokenRequest request)
        {
            var user = DomainService.SignInWithToken(request.TokenId, new Guid(request.Token));
            return new SignInWithTokenResponse
            {
                Username = user.Username,
                Fullname = user.Fullname,
                ProfileImageUrl = "/img/a2.jpg"
            };
        }

        public virtual SignOutResponse SignOut()
        {
            DomainService.SignOut();
            return new SignOutResponse();
        }
    }
}