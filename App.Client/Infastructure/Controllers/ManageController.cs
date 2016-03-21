using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Client.Infastructure.Atttributes;
using App.Client.Infastructure.Managers;
using App.Client.Infastructure.ViewModels;
using Microsoft.AspNet.Identity;

namespace App.Client.Infastructure.Controllers
{
    [Permit]
    [RoutePrefix("Me")]
    public class ManageController : BaseController
    {
        #region Construct 

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        #endregion

        #region Change Password

        [Route("Change-Password")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Route("Change-Password")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var changePassword = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (!changePassword.Succeeded)
                {
                    GetAlert(Danger, changePassword.Errors.First());
                }
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    GetAlert(Success, "Password Changed.");
                    await SignInAsync(user, isPersistent: false);
                }
                return View(model);
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View(model);
        }

        #endregion
    }
}