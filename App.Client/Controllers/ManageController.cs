using System.Linq;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Client.Atttributes;
using App.Client.Managers;
using App.Client.ViewModels;

namespace App.Client.Controllers
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