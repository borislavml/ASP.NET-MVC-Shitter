namespace Shitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Shitt
    {
        private const int DEFAULT_RESHITTS = 0;

        private int reshitts;
        private bool reported;
        private DateTime createdOn;

        private ICollection<User> usersFavourite;
        private ICollection<Comment> comments;

        public Shitt()
        {
            this.Reshitts = DEFAULT_RESHITTS;
            this.Reported = false;
            this.CreatedOn = DateTime.Now;

            this.usersFavourite = new HashSet<User>();
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public string ImageDataUrl { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn
        {
            get { return this.createdOn; }
            set { this.createdOn = value; }
        }

        [DefaultValue(DEFAULT_RESHITTS)]
        public int Reshitts
        {
            get { return this.reshitts; }
            set { this.reshitts = value; }
        }

        [DefaultValue(false)]
        public bool Reported
        {
            get { return this.reported; }
            set { this.reported = value; }
        }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        public virtual ICollection<User> UsersFavourite
        {
            get { return this.usersFavourite; }
            set { this.usersFavourite = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
