using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class Comment
    {
        public Comment()
        {
            this.CommentDate = DateTime.Now;
        }
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        [DefaultValue(0)]
        public int ComentsLikeCounter { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }

    }
}