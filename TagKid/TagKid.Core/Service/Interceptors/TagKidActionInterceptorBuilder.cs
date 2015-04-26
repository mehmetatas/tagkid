using TagKid.Framework.Repository;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Interceptors
{
    public class TagKidActionInterceptorBuilder : IActionInterceptorBuilder
    {
        private readonly IUnitOfWork _uow;

        public TagKidActionInterceptorBuilder(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionInterceptor Build(RouteContext context)
        {
            return new TagKidActionInterceptor(_uow);
        }
    }
}
