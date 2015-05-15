namespace Shitter.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Linq;

    using Shitter.Data;
    using Shitter.Models;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        private const int ALLOWED_FILE_SIZE = 524288;

        public override bool IsValid(object value)
        {
            var username = value as string;
            var db = new ShitterDbContext();

            var user = db.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                return false;   
            }

            return true;
        }
    }
}