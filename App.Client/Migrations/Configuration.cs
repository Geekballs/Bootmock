using App.Client.Infastructure.Contexts;

namespace App.Client.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext ctx)
        {
            SeedData.DefaultUser(ctx);
            //SeedData.AuditData(ctx);
        }
    }
}
