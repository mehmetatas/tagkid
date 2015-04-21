
namespace TagKid.Framework.WebApi
{
    public interface IActionInterceptorBuilder
    {
        IActionInterceptor Build(RouteContext context);
    }
}
