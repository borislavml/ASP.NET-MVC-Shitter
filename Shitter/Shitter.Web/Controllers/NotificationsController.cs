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
    using Shitter.Web.Models.Notifications;
    using Shitter.Web.Models.Shitts;
    using Shitter.Web.Models.Users;

    public class NotificationsController : BaseController
    {

        public NotificationsController(IShiterData data)
            : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetNotificationsList()
        {
            var currentUserId = this.UserProfile.Id;

            var notificationsList = this.Data.Notifications.All()
                .Include(n => n.Sender)
                .Where(n => n.ReceiverId == currentUserId && n.Status == 0)
                .Select(NotificationViewModel.ViewModel)
                .OrderByDescending(n => n.DateSent)
                .ToList();

            return this.PartialView("_NotificationsListPartial", notificationsList);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteNotification(int id)
        {
            var notificationToDelete = this.Data.Notifications.All()
                .FirstOrDefault(n => n.Id == id);

            this.Data.Notifications.Delete(notificationToDelete);
            this.Data.SaveChanges();

            return this.Content("Deleted successffully");
        }
    }
}