using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class DirectivesController : BaseController
    {
        public ActionResult Post()
        {
            return PartialView();
        }

        public ActionResult Editor()
        {
            return PartialView();
        }

        public ActionResult TagSearch()
        {
            return PartialView();
        }
    }
}
