namespace Shitter.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Shitter.Models;
    using Shitter.Data.Migrations;
    public class ShitterDbContext : IdentityDbContext<User>, IShitterDbContext
    {
        public ShitterDbContext(): base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShitterDbContext, Configuration>());
        }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Shitt> Shitts { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public static ShitterDbContext Create()
        {
            return new ShitterDbContext();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
