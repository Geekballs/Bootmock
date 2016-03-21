using System.Web.Mvc;
using App.Client.Infastructure.Atttributes;

namespace App.Client.Infastructure.Controllers
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
