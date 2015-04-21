
namespace TagKid.Framework.WebApi
{
    public interface IParameterResolver
    {
        void Resolve(RouteContext ctx);
    }
}
