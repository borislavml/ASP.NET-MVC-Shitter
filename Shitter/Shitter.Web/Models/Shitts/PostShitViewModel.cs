namespace Shitter.Web.Models.Shitts
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Shitter.Web.Attributes;

    using Shitter.Models;

    public class PostShitViewModel
    {
        [Required]
        [StringLength(300, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string Content { get; set; }

        [DataType(DataType.Upload)]
        [FileExtension(ErrorMessage = "Only jpg, png and gif format allowed.")]
        [FileSize(ErrorMessage = "File size should be less than 500kb")]
        public HttpPostedFileBase ImageDataUrl { get; set; }
    }
}