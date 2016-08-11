using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TeamProject.DataModels
{
    public class PostImage
    {

        [Key]
        public int PostImageId { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

    }
}