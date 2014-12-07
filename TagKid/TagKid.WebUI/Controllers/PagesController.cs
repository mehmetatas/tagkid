using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult Timeline()
        {
            return PartialView();
        }

        public ActionResult SignUp()
        {
            return PartialView();
        }

        public ActionResult SignIn()
        {
            return PartialView();
        }

        public ActionResult ForgotPwd()
        {
            return PartialView();
        }
    }
}
