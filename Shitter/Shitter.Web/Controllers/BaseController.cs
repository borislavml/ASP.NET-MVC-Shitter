namespace Shitter.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Shitter.Models;
    using Shitter.Data.UnitOfWork;

    public abstract class BaseController : Controller
    {
        private IShiterData data;

        private User userProfile;

        protected BaseController(IShiterData data)
        {
            this.Data = data;
        }

        protected BaseController(IShiterData data, User userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        protected User UserProfile 
        {
            get { return this.userProfile;  }
            private set { this.userProfile = value; }
        }

        protected IShiterData Data 
        {
            get { return this.data;  }
            private set { this.data = value; }
        }

        protected override System.IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, System.AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}