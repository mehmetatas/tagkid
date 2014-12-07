using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class ModalsController : Controller
    {

        public ActionResult NewCategory()
        {
            return PartialView();
        }
    }
}
