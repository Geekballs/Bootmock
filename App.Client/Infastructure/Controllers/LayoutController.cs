using System.Web.Mvc;
using App.Client.Infastructure.ViewModels;
using Microsoft.AspNet.Identity;

namespace App.Client.Infastructure.Controllers
{
    public class LayoutController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Header()
        {
            var userId = User.Identity.GetUserId();
            var model = new CurrentUserVm();
            ViewBag.AppName = AppConfig.AppName;
            bool themeInverse = AppConfig.ThemeInverse;

            if (!themeInverse.Equals(true))
            {
                ViewBag.Inverse = "navbar-default";
            }
            else
            {
                ViewBag.Inverse = "navbar-inverse";
            }

            if (User.Identity.IsAuthenticated)
            {
                model.IsAuthenticated = true;
                model.Username = User.Identity.Name;
                model.IsAdmin = UserManager.IsInRole(userId, "Admin");
                model.IsDashboard = UserManager.IsInRole(userId, "Dashboard");
                model.IsInvestigate = UserManager.IsInRole(userId, "Investigate");
                return PartialView("_Header", model);
            }
            return null;
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            ViewBag.AppName = AppConfig.AppName;
            ViewBag.AppVersion = AppConfig.AppVersion;
            ViewBag.AppEnvironment = AppConfig.AppEnvironment;
            ViewBag.CompanyName = AppConfig.CompanyName;

            return PartialView("_Footer");
        }
    }
}