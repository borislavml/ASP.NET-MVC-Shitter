namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;
    using System.Web.Routing;

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
            ViewBag.UserImage = currentUser.ImageDataUrl;

            // get user's following list names 
            List<string> userFollowing = this.UserProfile.Following.Select(f => f.UserName).ToList();
            userFollowing.Add(currentUser.UserName);

            // get shitts owned by user's following
            var matches = from shitt in this.Data.Shitts.All()
                          where userFollowing.Contains(shitt.Owner.UserName)
                          orderby shitt.CreatedOn descending
                          select shitt;

            // cast matches to ShittViewModel
            IEnumerable<ShittViewModel> shitts = matches.Select(match => new ShittViewModel()
                     {
                         Id = match.Id,
                         ImageDataUrl = match.ImageDataUrl,
                         Content = match.Content,
                         CreatedOn = match.CreatedOn,
                         Reshitts = match.Reshitts,
                         OwnerImageDataUrl = match.Owner.ImageDataUrl,
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


        [HttpGet]
        [Authorize]
        public ActionResult EditProfile()
        {
            ViewBag.CurrentUser = this.UserProfile;
            ViewBag.UserSummary = this.UserProfile.Summary;
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserEditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userToEdit = this.Data.Users.All()
                    .FirstOrDefault(u => u.Id == this.UserProfile.Id);

                userToEdit.FullName = model.FullName;
                userToEdit.Country = model.Country;
                userToEdit.Town = model.Town;
                userToEdit.Summary = model.Summary;
                userToEdit.Website = model.Website;

                // upload image if existing
                if (model.ImageDataUrl != null && model.ImageDataUrl.ContentLength > 0)
                {
                    this.DeleteUserPhoto();

                    var file = Request.Files[0];
                    string pathToSave = Server.MapPath("~/Content/Images/Users/");
                    string filename = Path.GetFileName(file.FileName);

                    //ad current datetime to filename for uniqueness and save to file system
                    string curentDateTimeString = DateTime.Now.ToString("hh-mm-ss-yyyy-mm-d");
                    filename = curentDateTimeString + filename;
                    file.SaveAs(Path.Combine(pathToSave, filename));

                    // ad photo path in database
                    userToEdit.ImageDataUrl = "/Content/Images/Users/" + filename;                   
                }

                // check if image is deleted
                if (model.ImageDeleted == "yes")
                {
                    this.DeleteUserPhoto();
                    userToEdit.ImageDataUrl = "/Content/Images/no-image.png";                 
                }

                this.Data.SaveChanges();
                string sucessMessage = "Profile edited successfuly!";
                return this.RedirectToAction("Success", "Home", new { message = sucessMessage });
            }

            return this.RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            var result = this.Data.Users.All()
                .Where(u => u.UserName.ToLower().Contains(query.ToLower())
                    || u.FullName.ToLower().Contains(query.ToLower()))
                    .Select(u => new UserLinkViewModel
                    {
                        UserName = u.UserName,
                        FullName = u.FullName,
                        ImageDataUrl = u.ImageDataUrl,
                    }).ToList();

            return this.PartialView("_UsersResult", result);
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return this.View();
        }

        [HttpGet]
        public ActionResult Success(string message)
        {
            ViewBag.Message = message;
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

        private void DeleteUserPhoto()
        {
            // delete image from file system
            string fullPath = Request.MapPath("~" + this.UserProfile.ImageDataUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}