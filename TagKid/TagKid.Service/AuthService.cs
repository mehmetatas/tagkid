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

        public virtual SignUpResponse SignUpWithEmail(SignUpWithEmailRequest request)
        {
            DomainService.SignUpWithEmail(request.Email, request.Username, request.Password, request.Fullname);
            return new SignUpResponse();
        }
    }
}