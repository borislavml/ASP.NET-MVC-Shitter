namespace Shitter.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string[] validImageTypes = new string[]
        {
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png"
        };

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file != null)
            {
                var fileType = file.ContentType;
                var isValidFileType= validImageTypes.Contains(fileType);
                return isValidFileType;
            }

            return true;
        }
    }
}