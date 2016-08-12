using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class PostLike
    {
        [Key]
        public int PostLikeId { get; set; }
        [Required]
        public string UserName { get; set; }
        [DefaultValue(false)]
        public bool Like { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public PostLike()
        {
            this.Like = false;
        }
    }
}