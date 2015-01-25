using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class LayoutController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return PartialView();
        }

        public ActionResult Aside()
        {
            return PartialView();
        }

        public ActionResult Nav()
        {
            return PartialView();
        }

        public ActionResult Header()
        {
            return PartialView();
        }
    }
}
