namespace Shitter.Web.Models.Users
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Shitter.Models;
    using Shitter.Web.Models.Shitts;

    public class UserProfileMiniViewModel
    {
        public static Expression<Func<User, UserProfileMiniViewModel>> ViewModel
        {
            get
            {
                return x => new UserProfileMiniViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    ImageDataUrl = x.ImageDataUrl,
                    Summary = x.Summary,
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string ImageDataUrl { get; set; }
   
        public string Summary { get; set; }

        public bool UserIsFollowing { get; set; }
    }
}