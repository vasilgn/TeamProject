using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TeamProject.DataModels
{
    public abstract class Image
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "ImageUrl")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public  virtual ApplicationUser User { get; set; }

    }
}