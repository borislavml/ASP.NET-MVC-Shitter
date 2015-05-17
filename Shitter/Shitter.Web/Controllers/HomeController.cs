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
        private const int PAGE_SIZE = 10;

        public HomeController(IShiterData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            if (this.UserProfile != null)
            {
                return this.RedirectToAction("Dashboard", "Home");
            }

            var shitts = this.Data.Shitts.All()
                .Select(ShittViewModel.ViewModel)
                .OrderByDescending(s => s.CreatedOn);

            int pageSize = PAGE_SIZE;
            int pageNumber = page ?? 1;

            PagedList<ShittViewModel> model = new PagedList<ShittViewModel>(shitts, pageNumber, pageSize);
            return this.View(model);
        }

        [Authorize]
        public ActionResult Dashboard(int? page)
        {
            var currentUser = this.UserProfile;
            ViewBag.CurrentUser = currentUser;
            ViewBag.UserImage = (currentUser.ImageDataUrl != null) ? currentUser.ImageDataUrl : "/Content/Images/no-image.png";

            // get user's following list names 
            List<string> userFollowing = this.UserProfile.Following.Select(f => f.UserName).ToList();
            userFollowing.Add(currentUser.UserName);

            // get shitts owned by user's following
            var matches = from shitt in this.Data.Shitts.All()
                           where userFollowing.Contains(shitt.Owner.UserName)         
                           orderby shitt.CreatedOn descending
                           select shitt;

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

            int pageSize = PAGE_SIZE;
            int pageNumber = page ?? 1;

            PagedList<ShittViewModel> model = new PagedList<ShittViewModel>(shitts, pageNumber, pageSize);
            return this.View(model);
        }

        // POST: /Home/PostShitt
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostShitt(PostShitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shitt = new Shitt
                {
                    Content = model.Content,
                    OwnerId = this.UserProfile.Id,    
                    CreatedOn = DateTime.Now,
                };

                // upload image if existing
                if (model.ImageDataUrl != null && model.ImageDataUrl.ContentLength > 0)
                {
                    string base64String = this.ConvertImageToBase64String(model.ImageDataUrl);
                    shitt.ImageDataUrl = base64String;
                }

                try
                {
                    this.UserProfile.PostedShitts.Add(shitt);
                    this.Data.Shitts.Add(shitt);
                    this.Data.SaveChanges();
                }
                catch (Exception ex)
                {
                    return this.RedirectToAction("Error", "Home");             
                }           
            }

            return this.RedirectToAction("Dashboard", "Home");
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

        [HttpGet]
        public ActionResult Error()
        {
            ViewBag.Message = "Ooops, sorry something went wrong!";
            return this.View();
        }
    }
}