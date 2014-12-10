using System;
using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using Taga.Core.Mapping;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.DTO.Messages.Auth;
using TagKid.Core.Service;
using IServiceProvider = Taga.Core.IoC.IServiceProvider;

namespace TagKid.Service
{
    [Intercept]
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _prov;

        public AuthService(IMapper mapper)
        {
            _prov = ServiceProvider.Provider;
            _mapper = mapper;
        }

        private IAuthDomainService DomainService
        {
            get { return _prov.GetOrCreate<IAuthDomainService>(); }
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
                ProfileImageUrl = "img/a2.jpg"
            };
        }

        public ActivateAccountResponse ActivateAccount(long ccid, string cc)
        {
            if (ccid < 1 || String.IsNullOrWhiteSpace(cc))
            {
                throw Errors.S_InvalidActivationCode.ToException();
            }
            DomainService.ActivateAccount(ccid, cc);
            return new ActivateAccountResponse();
        }
    }
}