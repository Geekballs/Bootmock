using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using App.Client.Infastructure.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Client.Infastructure.Contexts
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

            #region Identity

            db.Entity<IdentityUserClaim>().ToTable("Security_UserClaim").Property(x => x.Id).HasColumnName("ClaimId");
            db.Entity<IdentityUserRole>().ToTable("Security_UserRole");
            db.Entity<IdentityUserLogin>().ToTable("Security_UserLogin");
            db.Entity<IdentityRole>().ToTable("Security_Role").Property(x => x.Id).HasColumnName("RoleId");
            db.Entity<IdentityRole>().ToTable("Security_Role").Property(x => x.Name).HasColumnName("RoleName");
            db.Entity<ApplicationUser>().ToTable("Security_User").Property(x => x.Id).HasColumnName("UserId");

            #endregion

            #region Demo 

            db.Entity<DemoAuditData>().ToTable("Demo_AuditData");

            #endregion
        }

        public DbSet<DemoAuditData> DemoAuditData { get; set; }
    }
}