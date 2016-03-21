using System.Web.Mvc;

namespace App.Client.Infastructure.Controllers
{
    [Authorize]
    [RoutePrefix("acme")]
    public class AcmeController : BaseController
    {
        [Route("dashboard")]
        [Authorize(Roles = "Admin, Dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Route("group-information")]
        [Authorize(Roles = "Admin, Investigate")]
        public ActionResult GroupInformation()
        {
            return View();
        }
    }
}