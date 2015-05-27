namespace Shitter.Web.Models.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Shitter.Models;

    public class CommentViewModel
    {
        public static Expression<Func<Comment, CommentViewModel>> ViewModel
        {
            get
            {
                return x => new CommentViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    OwnerImageDataUrl = x.Owner.ImageDataUrl,
                    OwnerUsername = x.Owner.UserName,
                    OwnerName = x.Owner.FullName,
                    OwnerId = x.Owner.Id,
                };
            }
        }
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OwnerUsername { get; set; }

        public string OwnerImageDataUrl { get; set; }

        public string OwnerName { get; set; }

        public string OwnerId { get; set; }
    }
}