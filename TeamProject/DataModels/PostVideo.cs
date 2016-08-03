using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class PostVideo
    {
        [Key]
        public int VideoId { get; set; }
        [Required]
        [Display(Name = "VideoUrl")]
        [DataType(DataType.Url)]
        public string VideoUrl { get; set; }
        public string VideoItemName { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}