using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class PostLike
    {
        [Key]
        public int PostId { get; set; }

        public ApplicationUser User { get; set; }

        public bool Like { get; set; }
        public Post Post { get; set; }
    }
}