using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Client.Atttributes;
using App.Client.DomainModels;
using App.Client.Managers;
using App.Client.ViewModels;
using Microsoft.AspNet.Identity;

namespace App.Client.Controllers
{
    [Permit(Roles = "Admin")]
    [RoutePrefix("Admin/User")]
    public class UserController : BaseController
    {
        #region Construct 

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        #endregion

        #region All Users

        [Route("All")]
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        #endregion

        #region Create User

        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Create(AdminVm.CreateUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var createUser = await UserManager.CreateAsync(user, model.Password);
                if (!createUser.Succeeded)
                {
                    GetAlert(Danger, createUser.Errors.First());
                    return View();
                }
                GetAlert(Success, "User Created.");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion

        #region Edit User

        [Route("Edit/{id}")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                GetAlert(Danger, "Value Cannot Be Empty.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                GetAlert(Danger, "User Not Found.");
                return HttpNotFound();
            }
            var userRoles = await UserManager.GetRolesAsync(user.Id);
            return View(new AdminVm.EditUser()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AdminVm.EditUser model, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    GetAlert(Danger, "Value Cannot Be Empty.");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user.UserName = user.Email;
                user.Email = user.Email;
                user.EmailConfirmed = model.EmailConfirmed;
                var userRoles = await UserManager.GetRolesAsync(user.Id);
                selectedRole = selectedRole ?? new string[] { };
                var ammendUserRoles = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray());

                if (!ammendUserRoles.Succeeded)
                {
                    GetAlert(Danger, ammendUserRoles.Errors.First());
                    return View();
                }
                ammendUserRoles = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray());
                if (!ammendUserRoles.Succeeded)
                {
                    GetAlert(Danger, ammendUserRoles.Errors.First());
                    return View();
                }
                GetAlert(Success, "User Updated.");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion

        #region Delete User

        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                GetAlert(Danger, "Value Cannot Be Empty.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                GetAlert(Danger, "User Not Found.");
                return HttpNotFound();
            }
            return View(user);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    GetAlert(Danger, "Value Cannot Be Empty.");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    GetAlert(Danger, "User Not Found.");
                    return HttpNotFound();
                }
                var deleteUser = await UserManager.DeleteAsync(user);
                if (!deleteUser.Succeeded)
                {
                    GetAlert(Danger, deleteUser.Errors.First());
                    return View();
                }
                GetAlert(Success, "User Deleted.");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion

        #region User Password Overrride

        [Route("Reset-Password/{id}")]
        public async Task<ActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                GetAlert(Danger, "Value Cannot Be Empty.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                GetAlert(Danger, "User Not Found.");
                return HttpNotFound();
            }
            var model = new AdminVm.ResetUserPassword
            {
                Username = user.UserName
            };
            return View(model);
        }

        [Route("Reset-Password/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(AdminVm.ResetUserPassword model, string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    GetAlert(Danger, "Value Cannot Be Empty.");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    GetAlert(Danger, "User Not Found.");
                    return HttpNotFound();
                }
                var removePassword = await UserManager.RemovePasswordAsync(id);
                if (!removePassword.Succeeded)
                {
                    GetAlert(Danger, removePassword.Errors.First());
                    return View();
                }
                var resetCount = UserManager.ResetAccessFailedCount(id);
                if (!resetCount.Succeeded)
                {
                    GetAlert(Danger, resetCount.Errors.First());
                    return View();
                }
                var updatePwd = UserManager.AddPassword(id, model.NewPassword);
                if (!updatePwd.Succeeded)
                {
                    GetAlert(Danger, removePassword.Errors.First());
                    return View();
                }
                GetAlert(Success, "User Password Reset.");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion

        // Not implemented.
        public async Task<ActionResult> UserMenu(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            ViewBag.UserId = id;
            return PartialView("_UserMenu", user);
        }
    }
}
