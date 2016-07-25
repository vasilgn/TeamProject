using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.DataModels;

namespace TeamProject.Models
{
    public class ImageViewModel
    {
        [Key]
        public int Id { get; set; }

        public virtual UserImage Image { get; set; }

        public DateTime PostDate { get; set; }
        [AllowHtml]
        public string Contents { get; set; }
    }
}