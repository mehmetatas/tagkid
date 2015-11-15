
namespace TagKid.Framework.Hosting
{
    public interface IParameterResolver
    {
        void Resolve(RouteContext ctx);
    }
}
