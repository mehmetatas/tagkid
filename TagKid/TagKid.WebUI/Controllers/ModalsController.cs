using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class ModalsController : BaseController
    {
        public ActionResult TermsAndPolicy()
        {
            return PartialView();
        }
    }
}
