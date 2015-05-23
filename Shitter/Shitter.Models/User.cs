namespace Shitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;


    public class User : IdentityUser
    {
        private ICollection<User> followers;
        private ICollection<User> following;

        private ICollection<Shitt> postedShitts;
        private ICollection<Shitt> favouriteShitts;

        private ICollection<Notification> notificatioins;

        private ICollection<Message> sentMessages;
        private ICollection<Message> receivedMessages;

        public User()
        {
            this.followers = new HashSet<User>();
            this.following = new HashSet<User>();

            this.postedShitts = new HashSet<Shitt>();
            this.favouriteShitts = new HashSet<Shitt>();

            this.notificatioins = new HashSet<Notification>();

            this.sentMessages = new HashSet<Message>();
            this.receivedMessages = new HashSet<Message>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        public string ImageDataUrl { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string Country { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string Town { get; set; }

        [MinLength(1)]
        [MaxLength(100)]
        public string Summary { get; set; }

        [Url]
        [MaxLength(50)]
        public string Website { get; set; }

        public virtual ICollection<User> Followers 
        {
            get { return this.followers; }
            set { this.followers = value;}
        }

        public virtual ICollection<User> Following 
        {
            get { return this.following; }
            set { this.following = value; }
        }
        
        [InverseProperty("Owner")]
        public virtual ICollection<Shitt> PostedShitts
        {
            get { return this.postedShitts; }
            set { this.postedShitts = value; }
        }

        [InverseProperty("UsersFavourite")]
        public virtual ICollection<Shitt> FavouriteShitts
        {
            get { return this.favouriteShitts; }
            set { this.favouriteShitts = value; }
        }

        public virtual ICollection<Notification> Notifications 
        {
            get { return this.notificatioins; }
            set { this.notificatioins = value; }
        }

        [InverseProperty("Sender")]
        public virtual ICollection<Message> SentMessages
        {
            get { return this.sentMessages; }
            set { this.sentMessages = value; }
        }

        [InverseProperty("Receiver")]
        public virtual ICollection<Message> ReceivedMessages
        {
            get { return this.receivedMessages; }
            set { this.receivedMessages = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
