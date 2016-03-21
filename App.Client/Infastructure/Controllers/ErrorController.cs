using System.Web.Mvc;

namespace App.Client.Infastructure.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Generic()
        {
            return View();
        }

        public ActionResult Lockout()
        {
            return View();
        }

        public ActionResult Http400()
        {
            return View();
        }

        public ActionResult Http404()
        {
            return View();
        }

        public ActionResult Http500()
        {
            return View();
        }
    }
}