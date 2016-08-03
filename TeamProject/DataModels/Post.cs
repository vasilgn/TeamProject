using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TeamProject.DataModels
{
    public class Post
    {
        public Post()
        {
            this.PostedOn = DateTime.Now;
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int PostId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Body")]
        public string Body { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? Modified { get; set; }
        [DefaultValue(0)]
        public int PostLikeCounter { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [DefaultValue(true)]
        public bool IsPublic { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostVideo> PostVideo { get; set; }
        public ICollection<PostImage> PostImages { get; set; }
    }
}