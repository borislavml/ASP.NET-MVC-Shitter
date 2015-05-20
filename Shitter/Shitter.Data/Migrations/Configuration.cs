namespace Shitter.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Shitter.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<ShitterDbContext>
    {
        public Configuration()
        {
           this.AutomaticMigrationsEnabled = true;
           this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Shitter.Data.ShitterDbContext context)
        {
            if (context.Users.Any())
            {
                // Seed initial data only if the database is empty
                return;
            }

            this.SeedUsers(context);
        }

        private IList<User> SeedUsers(ShitterDbContext context)
        {
            var usernames = new string[] { "admin", "bot",};

            var users = new List<User>();
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 2,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            foreach (var username in usernames)
            {
                var name = username[0].ToString().ToUpper() + username.Substring(1);
                var user = new User
                {
                    UserName = username,
                    FullName = name,
                    Email = username + "@gmail.com",
                    ImageDataUrl = "/Content/Images/Users/no-image.png",
                    RegistrationDate = DateTime.Now
                };

                var password = username;
                var userCreateResult = userManager.Create(user, password);
                if (userCreateResult.Succeeded)
                {
                    users.Add(user);
                }
                else
                {
                    throw new Exception(string.Join("; ", userCreateResult.Errors));
                }
            }

            context.SaveChanges();

            return users;
        }
    }
}
