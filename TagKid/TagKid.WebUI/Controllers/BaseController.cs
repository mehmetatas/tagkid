using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public abstract class BaseController : Controller
    {
    }
}