using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class Post
    {
        public Post()
        {
            this.IsPublic = true;
            this.PostDate = DateTime.Now;
            //this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime PostDate { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}