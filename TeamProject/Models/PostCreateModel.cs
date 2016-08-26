using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [DataType(DataType.Url)]
        public string VideoUrl { get; set; }
        [Display(Name = "IsPublic?")]
        public bool IsPublic { get; set; }
    }
}