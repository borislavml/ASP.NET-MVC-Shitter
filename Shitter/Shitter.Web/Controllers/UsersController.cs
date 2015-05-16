namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    using Shitter.Models;
    using Shitter.Data.UnitOfWork;
    using Shitter.Web.Models.Users;
    using Shitter.Web.Models.Shitts;

    public class UsersController : BaseController
    {
        public UsersController(IShiterData data)
            : base(data)
        {
        }

        // GET: userprofile
        public ActionResult Index(string username)
        {
            if (String.IsNullOrEmpty(username) && User.Identity.IsAuthenticated)
            {
                username = User.Identity.GetUserName();
            }

            var user= this.Data.Users.All()
                .Include(u => u.PostedShitts)
                .Where(u => u.UserName == username)
                .Select(UserProfileViewModel.ViewModel)   
                .FirstOrDefault();

            if (user == null)
            {
                return this.RedirectToAction("NotFound", "Home");
            }

            return this.View(user);
        }
    }
}