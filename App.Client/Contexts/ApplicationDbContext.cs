using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using App.Client.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Client.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder db)
        {
            base.OnModelCreating(db);
            db.Conventions.Remove<PluralizingTableNameConvention>();
            db.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(x => x.Id).HasColumnName("ClaimId");
            db.Entity<IdentityUserRole>().ToTable("UserRole");
            db.Entity<IdentityUserLogin>().ToTable("UserLogin");
            db.Entity<IdentityRole>().ToTable("Role").Property(x => x.Id).HasColumnName("RoleId");
            db.Entity<IdentityRole>().ToTable("Role").Property(x => x.Name).HasColumnName("Title");
            db.Entity<ApplicationUser>().ToTable("User").Property(x => x.Id).HasColumnName("UserId");
   
        }
  
    }
}