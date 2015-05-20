namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.AspNet.Identity;

    using Shitter.Models;
    using Shitter.Data.UnitOfWork;
    using Shitter.Web.Models.Users;
    using Shitter.Web.Models.Shitts;

    using PagedList;
    

    public class UsersController : BaseController
    {
        private const int PAGE_SIZE = 10;

        public UsersController(IShiterData data)
            : base(data)
        {
        }

        // GET: userprofile
        public ActionResult Profile(string username, int? page)
        {
            if (String.IsNullOrEmpty(username) && User.Identity.IsAuthenticated)
            {
                username = User.Identity.GetUserName();
            }

            var user = this.Data.Users.All()
                .Where(u => u.UserName == username)
                .Select(UserProfileViewModel.ViewModel)
                .FirstOrDefault();

            if (user == null)
            {
                return this.RedirectToAction("NotFound", "Home");
            }

            ViewBag.CurrentUser = user;
            ViewBag.DisplayFollowUnfollowButton = false;
            ViewBag.UserIsFollowing = false;

            // display follow/unfollow button if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.DisplayFollowUnfollowButton = (this.UserProfile.Id != user.Id);
                // check if user is following this user 
                List<string> followingList = this.UserProfile.Following.Select(f => f.UserName).ToList();
                bool userIsFollowing = followingList.Contains(username) ? true : false;
                ViewBag.UserIsFollowing = userIsFollowing;
            }

            var userShitts = this.Data.Shitts.All()
                .Where(s => s.OwnerId == user.Id)
                .Select(ShittViewModel.ViewModel)
                .OrderByDescending(s => s.CreatedOn);

            int pageSize = PAGE_SIZE;
            int pageNumber = page ?? 1;

            PagedList<ShittViewModel> model = new PagedList<ShittViewModel>(userShitts, pageNumber, pageSize);
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unfollow(string id)
        {
            var userToUnfollow = this.Data.Users.All()
                .FirstOrDefault(u => u.Id == id);

            var user = this.Data.Users.All()
                .FirstOrDefault(u => u.UserName == this.UserProfile.UserName);
            try
            {
                user.Following.Remove(userToUnfollow);
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return JavaScript("window.location = '/Home/error'");
            }

            ViewData["id"] = id;
            return this.PartialView("_FollowUserPartial");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Follow(string id)
        {
            var userToFollow = this.Data.Users.All()
                .FirstOrDefault(u => u.Id == id);

            var user = this.Data.Users.All()
                .FirstOrDefault(u => u.UserName == this.UserProfile.UserName);
            try
            {
                user.Following.Add(userToFollow);
                this.Data.SaveChanges();
            }

            catch (Exception ex)
            {

                return JavaScript("window.location = '/Home/error'");
            }

            ViewData["id"] = id;
            return this.PartialView("_UnfollowUserPartial");
        }

    }
}