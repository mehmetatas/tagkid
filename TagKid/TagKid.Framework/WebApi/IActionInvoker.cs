
namespace TagKid.Framework.WebApi
{
    public interface IActionInvoker
    {
        object InvokeAction(RouteContext ctx);
    }
}
