using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class PostDislike
    {
        [Key]
        public int PostId { get; set; }
        public ApplicationUser User { get; set; }
        public bool Dislike { get; set; }
        public Post Post { get; set; }
    }
}