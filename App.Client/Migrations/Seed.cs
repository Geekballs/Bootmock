using System;
using System.Data.Entity.Migrations;
using App.Client.Infastructure.Contexts;
using App.Client.Infastructure.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Client.Migrations
{
    internal static class SeedData
    {
        public static void DefaultUser(ApplicationDbContext db)
        {

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var name = AppConfig.DefaultUser;
            var pwd = AppConfig.DefaultUserPassword;
            const string adminRole = "Admin";
            const string dashboardRole = "Dashboard";
            const string investigateRole = "Investigate";

            //Create Role Admin if it does not exist
            var ar = roleManager.FindByName(adminRole);
            if (ar == null)
            {
                ar = new IdentityRole(adminRole);
                var roleresult = roleManager.Create(ar);
            }

            var dr = roleManager.FindByName(dashboardRole);
            if (dr == null)
            {
                dr = new IdentityRole(dashboardRole);
                var roleresult = roleManager.Create(dr);
            }

            var ir = roleManager.FindByName(investigateRole);
            if (ir == null)
            {
                ir = new IdentityRole(investigateRole);
                var roleresult = roleManager.Create(ir);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, EmailConfirmed = true };
                var createUser = userManager.Create(user, pwd);
                createUser = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains("Admin"))
            {
                var result = userManager.AddToRole(user.Id, "Admin");
            }
        }

        public static void AuditData(ApplicationDbContext db)
        {
            var curDt = DateTime.Today.AddDays(-7);
            const int numMin = 20160;
            var rnd = new Random();

            var i = 0;

            while (i < numMin)
            {
                db.DemoAuditData.AddOrUpdate(x => x.AuditDate,
                    new DemoAuditData()
                    {
                        AuditId = Guid.NewGuid(),
                        AuditDate = curDt.AddMinutes(i),
                        MinPerf = rnd.Next(0, 10),
                        MaxPerf = rnd.Next(5, 100),
                        AvgPerf = rnd.Next(100, 500),
                        Volume = rnd.Next(100, 500)
                    });
                i++;
            }
        }
    }
}
       



        


