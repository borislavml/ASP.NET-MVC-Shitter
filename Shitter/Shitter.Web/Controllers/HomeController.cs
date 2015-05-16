namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;

    using PagedList;

    using Shitter.Models;
    using Shitter.Data.UnitOfWork;
    using Shitter.Web.Models.Shitts;
    using Shitter.Web.Models.Users;

    public class HomeController : BaseController
    {
        public HomeController(IShiterData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (this.UserProfile != null)
            {
                return this.RedirectToAction("Dashboard", "Home");
            }


            //int pageSize = 5;
            //int shittsFromDb = id - 1;

            var shitts = this.Data.Shitts.All()
                .Select(ShittViewModel.ViewModel)
                .OrderByDescending(s => s.CreatedOn)
                .ToList();

            // PagedList<User> model = new PagedList<User>(shittsList, id, pageSize);
            return this.View(shitts);

        }

        [Authorize]
        public ActionResult Dashboard()
        {
            var currentUser = this.UserProfile;
            ViewBag.CurrentUser = currentUser;

            // get user's following list names 
            List<string> userFollowing = this.UserProfile.Following.Select(f => f.UserName).ToList();
            userFollowing.Add(currentUser.UserName);

            // get shitts owned by user's following
            var matches = (from shitt in this.Data.Shitts.All()
                           where userFollowing.Contains(shitt.Owner.UserName)         
                           orderby shitt.CreatedOn descending
                           select shitt).ToList();

            // cast matches to ShittViewModel
            IEnumerable<ShittViewModel> shitts = matches.Select( match => new ShittViewModel()
                     {
                         Id = match.Id,
                         ImageDataUrl = match.ImageDataUrl,
                         Content = match.Content,
                         CreatedOn = match.CreatedOn,
                         Reshitts = match.Reshitts,
                         OwnerImageDataUrl = match.Owner.ImageDataUrl == null ? "/Content/Images/no-image.png" : match.Owner.ImageDataUrl,
                         OwnerUsername = match.Owner.UserName,
                         OwnerName = match.Owner.FullName,
                         OwnerId = match.Owner.Id,
                         FavoureitesCount = match.UsersFavourite.Count,
                     });

            return this.View(shitts);
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return this.View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            ViewBag.Message = "Sory, this page doesn't exist!";
            return this.View();
        }
    }
}