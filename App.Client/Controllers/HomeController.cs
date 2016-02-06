using System.Web.Mvc;
using App.Client.Atttributes;

namespace App.Client.Controllers
{
    [Permit]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}
