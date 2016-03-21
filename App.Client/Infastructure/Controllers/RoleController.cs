using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Client.Infastructure.Atttributes;
using App.Client.Infastructure.DomainModels;
using App.Client.Infastructure.Managers;
using App.Client.Infastructure.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Client.Infastructure.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("Admin/Role")]
    public class RoleController : BaseController
    {
        #region Construct

        public RoleController()
        {
        }

        public RoleController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        #endregion

        #region All Roles

        [Route("All")]
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        #endregion

        #region Create Role

        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Create(RoleVm model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(model.Name);
                var createRole = await RoleManager.CreateAsync(role);
                if (!createRole.Succeeded)
                {
                    GetAlert(Danger, createRole.Errors.First());
                    return View();
                }
                GetAlert(Success, "Role Created.");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion

        #region Edit Role

        [Route("Edit/{id}")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                GetAlert(Danger, "Value Cannot Be Empty.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                GetAlert(Danger, "Role Not Found.");
                return HttpNotFound();
            }
            var model = new RoleVm
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(model);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleVm model)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(model.Id);
                role.Name = model.Name;
                await RoleManager.UpdateAsync(role);
                GetAlert(Success, "Role Updated.");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion

        #region Role Users

        [Route("Users/{id}")]
        public async Task<ActionResult> Users(string id)
        {
            if (id == null)
            {
                GetAlert(Danger, "Value Cannot Be Empty.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            var users = new List<ApplicationUser>();
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }
            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        #endregion

        #region Delete Role

        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                GetAlert(Danger, "Value Cannot Be Empty.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                GetAlert(Danger, "Role Not Found.");
                return HttpNotFound();
            }
            return View(role);
        }

        [Route("Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult deleteRole;
                if (deleteUser != null)
                {
                    deleteRole = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    deleteRole = await RoleManager.DeleteAsync(role);
                }
                if (!deleteRole.Succeeded)
                {
                    GetAlert(Danger, deleteRole.Errors.First());
                    return View();
                }
                GetAlert(Success, "Role Deleted.");
                return RedirectToAction("Index");
            }
            GetAlert(Danger, "Model State Is Not Valid.");
            return View();
        }

        #endregion
    }
}
