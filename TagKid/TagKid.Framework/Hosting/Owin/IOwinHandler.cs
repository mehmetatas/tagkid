using System.Threading.Tasks;
using Microsoft.Owin;

namespace TagKid.Framework.Hosting.Owin
{
    public interface IOwinHandler
    {
        Task HandleRequestAsync(IOwinContext ctx);
    }
}