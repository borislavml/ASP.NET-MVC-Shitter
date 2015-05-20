using Microsoft.Owin;
using Owin;

using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(Shitter.Web.Startup))]
namespace Shitter.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
