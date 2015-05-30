namespace Shitter.Web.Models.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Shitter.Models;

    public class NotificationViewModel
    {
        public static Expression<Func<Notification, NotificationViewModel>> ViewModel
        {
            get
            {
                return x => new NotificationViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    DateSent = x.DateSent,
                    SenderImageDataUrl = x.Sender.ImageDataUrl,
                    SenderUserName = x.Sender.FullName,
                    SenderFullName = x.Sender.UserName,
                    Senderid = x.SenderId,
                    ShittId = x.ShittId,
                    FollowerName = x.FollowerName,
                    NotificationType = x.Type,
                };
            }
        }
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateSent { get; set; }

        public string SenderImageDataUrl { get; set; }

        public string SenderUserName { get; set; }

        public string SenderFullName { get; set; }

        public string Senderid { get; set; }

        public int? ShittId { get; set; }

        public string FollowerName { get; set; }

        public string NotificationType { get; set; }
    }
}