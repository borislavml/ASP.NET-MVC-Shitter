namespace Shitter.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Shitter.Web.Attributes;

    public class UserEditProfileViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        public string FullName { get; set; }

        [Display(Name="Country")]
        //[StringIsWhiteSpaces(ErrorMessage = "{0} can not be whitespaces only")]
        [StringLength(15, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Country { get; set; }

        [Display(Name = "Town")]
        //[StringIsWhiteSpaces(ErrorMessage = "{0} can not be whitespaces only")]
        [StringLength(15, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Town { get; set; }

        [Display(Name = "Summary")]
        //[StringIsWhiteSpaces(ErrorMessage = "{0} can not be whitespaces only")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        public string Summary { get; set; }

        [Url(ErrorMessage = "Please enter a valid url ex: http://website.org")]
        [Display(Name = "Website")]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Website { get; set; }

        [Display(Name = "Photo")]
        [DataType(DataType.Upload)]
        [FileExtension(ErrorMessage = "Only jpg, png and gif format allowed.")]
        [FileSize(ErrorMessage = "File size should be less than 500kb")]
        public HttpPostedFileBase ImageDataUrl { get; set; }

        public string ImageDeleted{ get; set; }
    }
}