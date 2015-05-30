namespace Shitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Enums;
    using System.ComponentModel;

    public class Notification
    {
        private const int DEFAULT_NOTIFICATION_STATUS = 0;

        private NotificationStatus status;
        private DateTime dateSent;

        public Notification()
        {
            this.status = DEFAULT_NOTIFICATION_STATUS;
            this.DateSent = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime DateSent
        {
            get { return this.dateSent; }
            set { this.dateSent = value;  }
        }

        [Required]
        [DefaultValue(DEFAULT_NOTIFICATION_STATUS)]
        public NotificationStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public int? ShittId { get; set; }

        public string FollowerName { get; set; }

        public string Type { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}
