namespace Shitter.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ShitterDbContext>
    {
        public Configuration()
        {
           this.AutomaticMigrationsEnabled = true;
           this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Shitter.Data.ShitterDbContext context)
        {
        }
    }
}
