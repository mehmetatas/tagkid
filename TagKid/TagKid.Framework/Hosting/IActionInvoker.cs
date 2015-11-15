namespace TagKid.Framework.Hosting
{
    public interface IActionInvoker
    {
        void InvokeAction(RouteContext ctx);
    }
}
