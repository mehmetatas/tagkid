using System.Threading;
using System.Web.Mvc;

namespace TagKid.WebUI.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult Timeline()
        {
            return PartialView();
        }

        public new ActionResult User()
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

        public ActionResult Activation()
        {
            return PartialView();
        }

        public ActionResult ForgotPwd()
        {
            return PartialView();
        }

        public ActionResult Tag()
        {
            return PartialView();
        }

        public ActionResult Category()
        {
            return PartialView();
        }
    }
}
