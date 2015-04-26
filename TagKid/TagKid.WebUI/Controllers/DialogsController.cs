using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class DialogsController : BaseController
    {
        public ActionResult Auth()
        {
            return PartialView();
        }
    }
}
