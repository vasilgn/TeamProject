using System.ComponentModel.DataAnnotations;

namespace TeamProject.Models
{
    public class PostCreateModel
    {
        [Required]
        [Display(Name = "Title *")]
        [StringLength(100, ErrorMessage = "Title is required or to long.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Body *")]
        [StringLength(1000, ErrorMessage = "Body is required or to long.")]
        public string Body { get; set; }


        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Short description.")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [Display(Name = "Video")]
        [DataType(DataType.Url,ErrorMessage ="Invalid url.")]
        [RegularExpression(@"^(?:https?\:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v\=))([\w-]{10,12})(?:$|\&|\?\#).*", ErrorMessage = "Invalid youtube link.")]
        public string VideoUrl { get; set; }
        [Display(Name = "IsPublic?")]
        public bool IsPublic { get; set; }
    }
}