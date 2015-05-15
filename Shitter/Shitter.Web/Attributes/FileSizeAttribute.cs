namespace Shitter.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileSizeAttribute : ValidationAttribute
    {
        private const int ALLOWED_FILE_SIZE = 524288; 

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file != null)
            {
                var filesSize = file.ContentLength;
                var isValidFileSize = filesSize < ALLOWED_FILE_SIZE;
                return isValidFileSize;
            }

            return true;
        }
    }
}