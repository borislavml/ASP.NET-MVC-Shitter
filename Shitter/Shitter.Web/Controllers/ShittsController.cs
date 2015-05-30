namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Shitter.Data.UnitOfWork;
    using Shitter.Models;
    using Shitter.Web.Models.Shitts;
    using Shitter.Web.Models.Users;

    public class ShittsController : BaseController
    {
        public ShittsController(IShiterData data)
            : base(data)
        {
        }

        // POST: /Shitts/PostShitt
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
                    var file = Request.Files[0];
                    string pathToSave = Server.MapPath("~/Images/Shitts/");
                    string filename = Path.GetFileName(file.FileName);

                    //ad current datetime to filename for uniqueness and save to file system
                    string curentDateTimeString = DateTime.Now.ToString("hh-mm-ss-yyyy-mm-d");
                    filename = curentDateTimeString + filename;
                    file.SaveAs(Path.Combine(pathToSave, filename));

                    // ad photo path in database
                    shitt.ImageDataUrl = "/Images/Shitts/" + filename;
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

        [Authorize]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteShitt(int id)
        {
            var shittToDelete = this.Data.Shitts.All()
                .FirstOrDefault(s => s.Id == id);
            string shittToDeletePhoto = shittToDelete.ImageDataUrl;

            // check if shitt is owned by current user
            if (shittToDelete.OwnerId == this.UserProfile.Id)
            {
                this.Data.Shitts.Delete(shittToDelete);
                this.Data.SaveChanges();

                if (shittToDeletePhoto != null)
                {
                    this.DeleteShittPhoto(shittToDeletePhoto);
                }

                return Json(id);
            }

            return Json(id);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FavouriteShitt(int id)
        {
            var currentUser = this.UserProfile;
            var shittToFavourtie = this.Data.Shitts.All()
                .FirstOrDefault(s => s.Id == id);

            // check if shitt isn't already favourtite for this user
            List<string> favouritesList = shittToFavourtie.UsersFavourite.Select(f => f.UserName).ToList();
            bool shittIsFavourite = favouritesList.Contains(currentUser.UserName) ? true : false;

            if (!shittIsFavourite)
            {
                shittToFavourtie.UsersFavourite.Add(currentUser);

                //send notifcation if like is not by owner of shitt
                if (this.UserProfile.Id != shittToFavourtie.OwnerId)
                {
                    var notification = new Notification
                    {
                        Content = "liked your shit",
                        DateSent = DateTime.Now,
                        SenderId = this.UserProfile.Id,
                        ReceiverId = shittToFavourtie.OwnerId,
                        Type = "comment",
                        ShittId = shittToFavourtie.Id
                    };

                    this.Data.Notifications.Add(notification);
                    shittToFavourtie.Owner.RecievedNotifications.Add(notification);
                    this.UserProfile.SentNotifications.Add(notification);
                }

                this.Data.SaveChanges();
                ViewData["id"] = id;

                return this.PartialView("_UnfavouriteShittButtonPartial");
            }

            return JavaScript("window.location = '/Home/error'");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnfavouriteShitt(int id)
        {
            var currentUser = this.UserProfile;
            var shittToUnavourtie = this.Data.Shitts.All()
                .FirstOrDefault(s => s.Id == id);

            // check if shitt isn't already unfavourtited for this user
            List<string> favouritesList = shittToUnavourtie.UsersFavourite.Select(f => f.UserName).ToList();
            bool shittIsFavourite = favouritesList.Contains(currentUser.UserName) ? true : false;

            if (shittIsFavourite)
            {
                shittToUnavourtie.UsersFavourite.Remove(currentUser);
                this.Data.SaveChanges();
                ViewData["id"] = id;

                return this.PartialView("_FavouriteShittButtonPartial");
            }

            return JavaScript("window.location = '/Home/error'");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetShittFavourites(int id)
        {
            var shitt = this.Data.Shitts.All()
                .FirstOrDefault(s => s.Id == id);

            var shittFavouritesList = shitt.UsersFavourite
                .Select(u => new UserLinkViewModel
                    {
                        UserName = u.UserName,
                        FullName = u.FullName,
                        ImageDataUrl = u.ImageDataUrl,
                    }).ToList();


            return this.PartialView("_ShittFavouritesListPartial", shittFavouritesList);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetShittById(int id)
        {
            var shittToReturn = this.Data.Shitts.All()
                .Include(s => s.Owner)
                .Include(s => s.UsersFavourite)
                .Where(s => s.Id == id)
                .Select(ShittViewModel.ViewModel)
                .FirstOrDefault();

            // check if returned shitt is liked by current user 
            var currentUserName = this.UserProfile.UserName;
            shittToReturn.IsFavourite = shittToReturn.UsersFavourite.Contains(currentUserName);

            return this.PartialView("_SingleShittPartialView", shittToReturn);
        }

        private void DeleteShittPhoto(string path)
        {
            // delete image from file system
            string fullPath = Request.MapPath("~" + path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}