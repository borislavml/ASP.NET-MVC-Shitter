namespace Shitter.Web.Models.Shitts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Shitter.Models;

    public class ShittViewModel
    {
        public static Expression<Func<Shitt, ShittViewModel>> ViewModel
        {
            get
            {
                return x => new ShittViewModel
                {
                    Id = x.Id,
                    ImageDataUrl = x.ImageDataUrl,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    Reshitts = x.Reshitts,
                    OwnerImageDataUrl = x.Owner.ImageDataUrl,
                    OwnerUsername = x.Owner.UserName,
                    OwnerName = x.Owner.FullName,
                    OwnerId = x.Owner.Id,
                    FavoureitesCount = x.UsersFavourite.Count,
                    UsersFavourite = x.UsersFavourite.Select(u => u.UserName).ToList(),
                    IsFavourite = false,
                };
            }
        }
        public int Id { get; set; }

        public string ImageDataUrl { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Reshitts { get; set; }

        public string OwnerUsername { get; set; }

        public string OwnerImageDataUrl { get; set; }

        public string OwnerName { get; set; }

        public string OwnerId { get; set; }

        public int FavoureitesCount { get; set; }

        public bool IsFavourite { get; set; }

        public  IEnumerable<string> UsersFavourite { get; set; }   
    }
}