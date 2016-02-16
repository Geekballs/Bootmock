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

            #region Identity

            db.Entity<IdentityUserClaim>().ToTable("Security-UserClaim").Property(x => x.Id).HasColumnName("ClaimId");
            db.Entity<IdentityUserRole>().ToTable("Security-UserRole");
            db.Entity<IdentityUserLogin>().ToTable("Security-UserLogin");
            db.Entity<IdentityRole>().ToTable("Security-Role").Property(x => x.Id).HasColumnName("RoleId");
            db.Entity<IdentityRole>().ToTable("Security-Role").Property(x => x.Name).HasColumnName("Title");
            db.Entity<ApplicationUser>().ToTable("Security-User").Property(x => x.Id).HasColumnName("UserId");

            #endregion

            #region Demo Products

            db.Entity<Chasis>().ToTable("Product-Chasis").Property(x => x.Id).HasColumnName("ChasisId");
            db.Entity<HardDrive>().ToTable("Product-HardDrive").Property(x => x.Id).HasColumnName("HarDriveId");
            db.Entity<Memory>().ToTable("Product-Memory").Property(x => x.Id).HasColumnName("MemoryId");
            db.Entity<NetworkAdapter>().ToTable("Product-NetworkAdapter").Property(x => x.Id).HasColumnName("NetworkAdapterId");
            db.Entity<OperatingSystem>().ToTable("Product-OperatingSystem").Property(x => x.Id).HasColumnName("OperatingSystemId"); ;
            db.Entity<Environment>().ToTable("Product-Environment").Property(x => x.Id).HasColumnName("EnvironmentId");

            #endregion
        }

    }
}