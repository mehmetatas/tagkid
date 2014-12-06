using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class UIController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Layout()
        {
            return PartialView();
        }

        public ActionResult Timeline()
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

        public ActionResult NewCategory()
        {
            return PartialView();
        }
    }
}
