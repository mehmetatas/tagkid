using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class PagesController : BaseController
    {
        public ActionResult Timeline()
        {
            return PartialView();
        }
    }
}
