namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.AspNet.Identity;

    using PagedList;

    using Shitter.Models;
    using Shitter.Data.UnitOfWork;
    using Shitter.Web.Models.Users;
    using Shitter.Web.Models.Shitts;

    public class UsersController : BaseController
    {
        private const int PAGE_SIZE = 10;
        private const int PAGE_SIZE_FOLLOWERS = 9;

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
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserViewingThePageName = this.UserProfile.UserName;
                ViewBag.UserViewingThePageId = this.UserProfile.Id;
            }

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
                .Include(s => s.Owner)
                .Include(s => s.UsersFavourite)
                .Where(s => s.OwnerId == user.Id)
                .Select(ShittViewModel.ViewModel)
                .OrderByDescending(s => s.CreatedOn);

            int pageSize = PAGE_SIZE;
            int pageNumber = page ?? 1;

            PagedList<ShittViewModel> model = new PagedList<ShittViewModel>(userShitts, pageNumber, pageSize);

            var currentUserName = User.Identity.Name;
            foreach (var item in model)
            {
                item.IsFavourite = item.UsersFavourite.Contains(currentUserName);
            }

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

            if (this.UserisFollowing(userToUnfollow.UserName))
            {
                user.Following.Remove(userToUnfollow);
                this.Data.SaveChanges();
                ViewData["id"] = id;

                return this.PartialView("_FollowUserPartial");
            }

            return JavaScript("window.location = '/Home/error'");
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

            if (!this.UserisFollowing(userToFollow.UserName))
            {
                user.Following.Add(userToFollow);
                this.Data.SaveChanges();

                ViewData["id"] = id;
                return this.PartialView("_UnfollowUserPartial");
            }

            return JavaScript("window.location = '/Home/error'");
        }

        [HttpGet]
        public ActionResult Following(string id, int? page)
        {
            //if (!Request.IsAjaxRequest())
            //{
            //    Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //    return JavaScript("window.location = '/Home/Error'");
            //}

            var userToGetFollowingList = this.Data.Users.All()
                .FirstOrDefault(u => u.Id == id);

            var followingList = userToGetFollowingList.Following
                .Select(f => new UserProfileMiniViewModel
                {
                    Id = f.Id,
                    UserName = f.UserName,
                    FullName = f.FullName,
                    ImageDataUrl = f.ImageDataUrl,
                    Summary = f.Summary,
                    UserIsFollowing = this.UserisFollowing(f.UserName),
                });

            int pageSize = PAGE_SIZE_FOLLOWERS;
            int pageNumber = page ?? 1;

            PagedList<UserProfileMiniViewModel> model = new PagedList<UserProfileMiniViewModel>(followingList, pageNumber, pageSize);

            ViewData["user-id"] = userToGetFollowingList.Id;
            return this.PartialView("_UserFollowingPartialView", model);
        }

        [HttpGet]
        public ActionResult Followers(string id, int? page)
        {
            var userToGetFollowersList = this.Data.Users.All()
                .FirstOrDefault(u => u.Id == id);

            var followersList = userToGetFollowersList.Followers
                .Select(f => new UserProfileMiniViewModel
                {
                    Id = f.Id,
                    UserName = f.UserName,
                    FullName = f.FullName,
                    ImageDataUrl = f.ImageDataUrl,
                    Summary = f.Summary,
                    UserIsFollowing = this.UserisFollowing(f.UserName),
                });

            int pageSize = PAGE_SIZE_FOLLOWERS;
            int pageNumber = page ?? 1;

            PagedList<UserProfileMiniViewModel> model = new PagedList<UserProfileMiniViewModel>(followersList, pageNumber, pageSize);

            ViewData["user-id"] = userToGetFollowersList.Id;
            return this.PartialView("_UserFollowersPartialView", model);
        }

        [HttpGet]
        public ActionResult Favourites(string id, int? page)
        {
            var userToGetFvourtiesList = this.Data.Users.All()
                .FirstOrDefault(u => u.Id == id);

            var favouritesList = userToGetFvourtiesList.FavouriteShitts
                .OrderByDescending(s => s.CreatedOn)
                .Select(s => new ShittViewModel
                {
                    Id = s.Id,
                    ImageDataUrl = s.ImageDataUrl,
                    Content = s.Content,
                    CreatedOn = s.CreatedOn,
                    Reshitts = s.Reshitts,
                    OwnerImageDataUrl = s.Owner.ImageDataUrl,
                    OwnerUsername = s.Owner.UserName,
                    OwnerName = s.Owner.FullName,
                    OwnerId = s.Owner.Id,
                    UsersFavourite = s.UsersFavourite.Select(u => u.UserName).ToList(),
                    FavoureitesCount = s.UsersFavourite.Count,
                    ShittCommentsCount = s.Comments.Count,
                });

            int pageSize = PAGE_SIZE;
            int pageNumber = page ?? 1;

            PagedList<ShittViewModel> model = new PagedList<ShittViewModel>(favouritesList, pageNumber, pageSize);
            
            var currentUserName = User.Identity.Name;
            foreach (var item in model)
            {
                item.IsFavourite = item.UsersFavourite.Contains(currentUserName);
            }

            ViewData["user-id"] = userToGetFvourtiesList.Id;
            return this.PartialView("_UserFavouritesPartialView", model);
        }

        [HttpGet]
        public ActionResult GetTopUsers()
        {
            var topUsersList = this.Data.Users.All()
                .Select(u => new UserLinkViewModel
                {
                    UserName = u.UserName,
                    FullName = u.FullName,
                    ImageDataUrl = u.ImageDataUrl,
                    ShitsCount = u.PostedShitts.Count(),
                })
                .OrderByDescending(u => u.ShitsCount)
                .Take(10)
                .ToList();

            return this.PartialView("_TopUsersPartialView", topUsersList);
        }

        private bool UserisFollowing(string username)
        {
            // check if user is authenticated to show follow/unfollow button
            if (User.Identity.IsAuthenticated)
            {
                //check if currently authenticated user is not currently displayed user
                if (this.UserProfile.UserName != username)
                {
                    // check if currently authenticated user is following currently displayed user
                    List<string> followingList = this.UserProfile.Following.Select(f => f.UserName).ToList();
                    bool userIsFollowing = followingList.Contains(username) ? true : false;

                    return userIsFollowing;
                }
            }

            return false;
        }
    }
}