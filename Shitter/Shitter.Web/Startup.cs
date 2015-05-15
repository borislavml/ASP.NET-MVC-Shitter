using Microsoft.Owin;
using Owin;

using System.Data.Entity;
using Shitter.Data;
using Shitter.Data.Migrations;

[assembly: OwinStartupAttribute(typeof(Shitter.Web.Startup))]
namespace Shitter.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShitterDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
