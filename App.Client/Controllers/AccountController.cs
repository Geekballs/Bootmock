using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using App.Client.Atttributes;
using App.Client.DomainModels;
using App.Client.ViewModels;

namespace App.Client.Controllers
{
    [Permit]
    public class AccountController : BaseController
    {

        #region Login

        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.AppName = AppConfig.AppName;
            return View();
        }

        [Route("Login")]
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.Email);
                if (user == null)
                {
                    GetAlert(Danger, "Login failed.");
                    return RedirectToAction("Login");
                }
                await UserManager.FindByIdAsync(user.Id);
                if (!UserManager.IsEmailConfirmed(user.Id))
                {
                    GetAlert(Danger, "Account Not Verified.");
                    return RedirectToAction("Login");
                }
                var status = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (status)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        GetAlert(Danger, "This account is currently locked out!"); // Change this message in a production environment.
                        return RedirectToAction("Login", "Account");
                    case SignInStatus.Failure:
                        break;
                    default:
                        return View(model);
                }
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion

        #region Logoff

        [Route("LogOff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Register

        [Route("Register")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.AppName = AppConfig.AppName;
            return View();
        }

        [Route("Register")]
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var registerResult = await UserManager.CreateAsync(user, model.Password);
                if (registerResult.Succeeded)
                {
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    if (Request.Url != null)
                    {
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                        {
                            userId = user.Id,
                            code = code
                        },
                            protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                        ViewBag.Link = callbackUrl;
                    }
                    return View("DisplayEmail");
                }
                GetAlert(Danger, registerResult.Errors.First());
            }
            return View(model);
        }

        #endregion

        #region Confirm Email

        [Route("Confirm-Email")]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var confirmEmail = await UserManager.ConfirmEmailAsync(userId, code);
            GetAlert(Info, "Email confirmed. You can now login.");
            if (confirmEmail.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return View("Error");
        }

        #endregion

        #region Forgot Password

        [Route("Forgot-Password")]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("Forgot-Password")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed.
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    userId = user.Id,
                    code = code
                }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View(model);
        }

        [Route("Forgot-Password-Confirmation")]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region Reset Password

        [Route("Reset-Password")]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [Route("Reset-Password")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                var resetPassword = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (resetPassword.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        [Route("Reset-Password-Complete")]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion
    }
}