using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class CommentLike
    {
        [Key]
        public int CommentLikeId { get; set; }
        public string UserName { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

    }
}