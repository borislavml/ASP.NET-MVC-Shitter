namespace Shitter.Web.Models.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    using Shitter.Web.Attributes;

    public class PostCommentViewModel
    {
        [Required]
        [AllowHtml]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        public int ShittId { get; set; }
    }
}