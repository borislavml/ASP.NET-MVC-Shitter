namespace Shitter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Shitter.Data.UnitOfWork;
    using Shitter.Models;
    using Shitter.Web.Models.Comments;
    using Shitter.Web.Models.Shitts;
    using Shitter.Web.Models.Users;

    public class CommentsController : BaseController
    {
        public CommentsController(IShiterData data)
            : base(data)
        {
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(PostCommentViewModel model)
        {

            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = model.Content,
                    CreatedOn = DateTime.Now,
                    ShittId = model.ShittId,
                    OwnerId = this.UserProfile.Id,
                };

                this.Data.Comments.Add(comment); 
                this.UserProfile.Comments.Add(comment);

                // send notification if comment is not by owner of shitt
                var shittToBeCommeted = this.Data.Shitts.All()
                    .FirstOrDefault(s => s.Id == model.ShittId);

                if (this.UserProfile.Id != shittToBeCommeted.OwnerId )
                {
                    var notification = new Notification
                    {
                        Content = "commented on your shit",
                        DateSent = DateTime.Now,
                        SenderId = this.UserProfile.Id,
                        ReceiverId = shittToBeCommeted.OwnerId,
                        Type = "comment",
                        ShittId = shittToBeCommeted.Id
                    };

                    this.Data.Notifications.Add(notification);
                    shittToBeCommeted.Owner.RecievedNotifications.Add(notification);
                    this.UserProfile.SentNotifications.Add(notification);
                }

                this.Data.SaveChanges();

                var commentToReturn = new CommentViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedOn = comment.CreatedOn,
                    OwnerImageDataUrl = comment.Owner.ImageDataUrl,
                    OwnerUsername = comment.Owner.UserName,
                    OwnerName = comment.Owner.FullName,
                    OwnerId = comment.Owner.Id,
                };

                return this.PartialView("_NewPostedCommentPartial", commentToReturn);
            }

            return JavaScript("window.location = '/Home/error'");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetShittComments(int id)
        {
            var shittComments = this.Data.Comments.All()
                .Include(c => c.Owner)
                .Where(c => c.ShittId == id)
                .Select(CommentViewModel.ViewModel)
                .OrderByDescending(c => c.CreatedOn)
                .ToList();

            return this.PartialView("_ShittCommentsListPartial", shittComments);
        }
    }
}