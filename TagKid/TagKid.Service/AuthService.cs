using Taga.Core.DynamicProxy;
using Taga.Core.Mapping;
using TagKid.Core.Domain;
using TagKid.Core.Models.DTO.Messages.Auth;
using TagKid.Core.Service;
using TagKid.Core.Validation;

namespace TagKid.Service
{
    [Intercept]
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IDomainServiceProvider _prov;

        public AuthService(IDomainServiceProvider prov, IMapper mapper)
        {
            _prov = prov;
            _mapper = mapper;
        }

        private IAuthDomainService DomainService
        {
            get { return _prov.GetService<IAuthDomainService>(); }
        }

        public virtual SignUpResponse SignUpWithEmail(SignUpWithEmailRequest request)
        {
            Validator.Validate(request);
            DomainService.SignUpWithEmail(request.Email, request.Username, request.Password, request.Fullname);
            return new SignUpResponse();
        }
    }
}