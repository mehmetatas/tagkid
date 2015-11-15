namespace TagKid.Framework.Hosting
{
    public interface IRouteResolver
    {
        RouteContext Resolve(IHttpRequest httpRequest);
    }
}
