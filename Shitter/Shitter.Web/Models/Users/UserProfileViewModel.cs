namespace Shitter.Web.Models.Users
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Shitter.Models;
    using Shitter.Web.Models.Shitts;

    public class UserProfileViewModel
    {
        public static Expression<Func<User, UserProfileViewModel>> ViewModel
        {
            get
            {
                return x => new UserProfileViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    ImageDataUrl = x.ImageDataUrl,
                    Email = x.Email,
                    Country = x.Country,
                    Town = x.Town,
                    Summary = x.Summary,
                    Website = x.Website,
                    RegistrationDate = x.RegistrationDate,
                    PostedShittsCount = x.PostedShitts.Count,
                    FollowingCount = x.Following.Count,
                    FollowersCount = x.Followers.Count,
                    FavouritesCount = x.FavouriteShitts.Count,
                    //PostedShitts = x.PostedShitts.AsQueryable()
                    //    .Select(ShittViewModel.ViewModel)
                    //    .OrderByDescending(s => s.CreatedOn)
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string ImageDataUrl { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Summary { get; set; }

        public string Website { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int PostedShittsCount { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int FavouritesCount { get; set; }
    }
}