namespace Shitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        private DateTime dateSent;

        public Notification()
        {
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
        public string UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
