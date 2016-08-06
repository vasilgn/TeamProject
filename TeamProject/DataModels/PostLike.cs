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
        public int PostLikeId { get; set; }
        public string UserName { get; set; }
        public bool Like { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}