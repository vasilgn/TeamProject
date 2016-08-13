using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class CommentLike
    {
        [Key]
        public int CommentLikeId { get; set; }
        [Required]
        public string UserName { get; set; }
        [DefaultValue(false)]
        public bool Like { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public CommentLike()
        {
            this.Like = false;
        }
    }
}